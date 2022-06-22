using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Urxxx.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class BaseEnemy : MonoBehaviour, ITarget, IDamageDealer
    {
        #region Property fields

        public float Speed => BaseSpeed * statModifier.SpeedModifier;
        public int ExpGain => (int)(BaseExp * GameController.Instance.ExpMultiplier);

        #endregion

        #region Protect serialized fields

        [SerializeField] protected GameObject Target;
        [SerializeField] protected float BaseSpeed = 30f;
        [SerializeField] protected float MaxHealth;
        [SerializeField] protected float Damage;
        [SerializeField] protected float AttackRate;
        [SerializeField] protected float AttackRange = 0.2f;
        [SerializeField] protected int BaseExp = 2;

        #endregion

        #region Protect nonserialized fields

        protected float CurrentHealth;
        protected Rigidbody2D RigidBody;
        protected CircleCollider2D Collider;
        protected Action<BaseEnemy> OnDeathAction;

        #endregion

        #region Private nonserialized fields

        private Animator animator;
        private List<BaseStatusEffect> statusEffectList = new List<BaseStatusEffect>();
        private StatModifier statModifier = new StatModifier(1f);
        private float attackRateTiming = 0;

        #endregion

        #region LifeCycle Method

        protected void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
            animator = GetComponent<Animator>();
            OnDeathAction += Death;
        }

        // Start is called before the first frame update
        protected void Start()
        {
            CurrentHealth = MaxHealth * GameController.Instance.HpMultiplier;
        }

        // Update is called once per frame
        protected void Update()
        {
            UpdateTarget();
            UpdateHitEffect();

            attackRateTiming -= Time.deltaTime;
            if (CheckAttackRange())
            {
                RigidBody.velocity = Vector2.zero;
                //Attack
                if (attackRateTiming <= 0)
                {
                    if (TryAttack()) attackRateTiming = 100f / AttackRate;
                }
            }
            else
            {
                MoveTowardTarget();
            }

            CheckDeadZone();
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var collider = GetComponent<CircleCollider2D>();
            Gizmos.DrawWireSphere(transform.position, AttackRange + collider.radius);
        }

        #endregion

        #region Public Method

        public void Setup(Action<BaseEnemy> onDeath)
        {
            OnDeathAction += onDeath;
        }

        public void ForceKill(bool isCallAction = false)
        {
            if (isCallAction)
            {
                OnDeathAction(this);
            }
            else
            {
                Death(this);
            }
        }

        #endregion

        #region Protect Method

        protected virtual void Death(BaseEnemy enemy)
        {
            animator.SetBool("IsDead", true);
        }

        protected void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion

        #region Private Method

        private void UpdateTarget()
        {
            if (GameController.Instance != null && Target == null)
            {
                Target = GameController.Instance.GetCurrentPlayer();
            }
        }

        private void CheckDeadZone()
        {
            var camVector = Camera.main.transform.position - transform.position;
            camVector.z = 0;
            if (camVector.magnitude > 6)
            {
                ForceKill(true);
            }
        }

        private void UpdateHitEffect()
        {
            statModifier.Reset();
            foreach (var statusEffect in statusEffectList)
            {
                statusEffect.UpdateEffect();
            }

            for (int i = 0; i < statusEffectList.Count; i++)
            {
                var statusEffect = statusEffectList[i];
                if (statusEffect.IsComplete())
                {
                    statusEffectList.Remove(statusEffect);
                    i--;
                }
            }
        }

        private bool CheckAttackRange()
        {
            Vector3 targetVector = Target.transform.position - transform.position;
            float range = targetVector.magnitude;

            if (range <= Collider.radius + AttackRange)
            {
                return true;
            }

            return false;
        }

        private bool TryAttack()
        {
            if (Target == null) return false;
            var target = Target.GetComponent<ITarget>();
            DamageSystem.Instance.DamagingTarget(this, target);
            return true;
        }

        private void MoveTowardTarget()
        {
            if (Target == null) return;
            Vector3 targetVector = Target.transform.position - transform.position;
            targetVector.x += Random.Range(-1f, 1f);
            targetVector.y += Random.Range(-1f, 1f);
            RigidBody.AddForce(targetVector.normalized * Speed);
            RigidBody.velocity = Vector3.ClampMagnitude(RigidBody.velocity, Speed);
            Debug.Log($"{RigidBody.velocity.magnitude / Speed}");
            animator.SetFloat("Speed", RigidBody.velocity.magnitude / Speed);
        }

        #endregion

        #region ITarget implement

        public void DamageTaken(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                GameController.Instance.GainExp(ExpGain);
                OnDeathAction(this);
            }
        }
        public Transform GetTargetTransform()
        {
            return transform;
        }

        public T GetTargetComponent<T>()
        {
            return GetComponent<T>();
        }

        public void ModifyStat(BaseStatusEffect stat)
        {
            //use stat to mod
            StatModifier statMod = stat.GetStatModifier();
            if (statMod == null) return;
            statModifier.SpeedModifier *= statMod.SpeedModifier;
        }

        #endregion

        #region IDamageDealer implement

        public float DealDamage()
        {
            return Damage;
        }

        public void AddHitEffect(List<BaseHitEffect> hitEffects, Vector3 direction)
        {
            foreach (var hitEffect in hitEffects)
            {
                var statusEffect = hitEffect.CreateStatusEffect(direction);
                statusEffect.SetOwner(this);
                statusEffectList.Add(statusEffect);
            }
        }

        #endregion
    }
}