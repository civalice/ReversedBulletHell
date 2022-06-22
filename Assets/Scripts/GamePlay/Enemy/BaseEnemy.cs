using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

namespace Urxxx.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class BaseEnemy : MonoBehaviour, ITarget, IDamageDealer
    {
        #region Protect serialized fields

        [SerializeField] protected GameObject Target;
        [SerializeField] protected float Speed = 0.3f;
        [SerializeField] protected float MaxHealth;
        [SerializeField] protected float Damage;
        [SerializeField] protected float AttackRate;
        [SerializeField] protected float AttackRange = 0.2f;

        #endregion

        #region Protect nonserialized fields

        protected float CurrentHealth;
        protected Rigidbody2D RigidBody;
        protected CircleCollider2D Collider;
        protected Action<BaseEnemy> OnDeathAction;

        #endregion

        #region Private nonserialized fields

        private List<BaseStatusEffect> statusEffectList = new List<BaseStatusEffect>();

        #endregion

        private float attackRateTiming = 0;

        #region LifeCycle Method

        protected void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();
            OnDeathAction += Death;
        }

        // Start is called before the first frame update
        protected void Start()
        {
            CurrentHealth = MaxHealth;
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

        #endregion

        #region Protect Method

        protected virtual void Death(BaseEnemy enemy)
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

        private void UpdateHitEffect()
        {
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
            RigidBody.velocity = Time.deltaTime * targetVector.normalized * Speed;
        }

        #endregion

        #region ITarget implement

        public void DamageTaken(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
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
        }

        #endregion

        #region IDamageDealer implement

        public float DealDamage()
        {
            return Damage;
        }

        public void AddHitEffect(List<BaseHitEffect> hitEffects)
        {
            foreach (var hitEffect in hitEffects)
            {
                var statusEffect = hitEffect.CreateStatusEffect();
                statusEffect.SetOwner(this);
                statusEffectList.Add(statusEffect);
            }
        }

        #endregion

    }
}