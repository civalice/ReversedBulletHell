using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

namespace Urxxx.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class BaseEnemy : MonoBehaviour, ITarget
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

        private float fireRateTiming = 0;

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
            if (CheckAttackRange())
            {
                //Attack
                RigidBody.velocity = Vector2.zero;
            }
            else
            {
                MoveTowardTarget();
            }
        }

        protected void FixedUpdate()
        {
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

        #endregion
    }
}