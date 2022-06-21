using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public abstract class BaseBullet : MonoBehaviour
    {
        #region Protect serialized fields

        [SerializeField] protected GameObject HitEffectPrefab;
        [SerializeField] protected float BulletRange = 10f;
        [SerializeField] protected float Damage = 1.0f;
        [SerializeField] protected float ProjectileSpeed = 3.0f;
        [SerializeField] protected int PiecingCount = 1;

        #endregion

        #region Protect nonserialized fields

        protected Vector3 StartPosition;
        protected Vector3 TargetDirection;
        protected bool IsBulletLaunch = false;

        #endregion

        #region LifeCycle Method

        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            if (!IsBulletLaunch) return;
            if (IsComplete()) Destroy(gameObject);
        }

        #endregion

        #region Public Method

        public void AddHitEffect(BaseHitEffect hitEffect)
        {
            //Todo: Add Hit Effect
        }

        public virtual void SetupBullet(float damage, float speed, int piecingCount)
        {
            Damage = damage;
            ProjectileSpeed = speed;
            PiecingCount = piecingCount;
            StartPosition = transform.position;
        }

        public virtual void SetDirection(Vector3 targetDirection)
        {
            TargetDirection = targetDirection;
            IsBulletLaunch = true;
        }

        #endregion

        #region Protect Method

        protected virtual bool IsComplete()
        {
            return (transform.position - StartPosition).magnitude > BulletRange;
        }

        protected virtual void PlayHitEffect(Vector2 hitPosition)
        {
            if (HitEffectPrefab == null) return;
            var randomVal = 0.02f;
            var hitEffect = Instantiate(HitEffectPrefab,
                hitPosition + new Vector2(Random.Range(-randomVal, randomVal),
                    Random.Range(-randomVal, randomVal)), Quaternion.identity);
        }

        protected virtual void HitTarget(RaycastHit2D hitTarget)
        {
            var enemy = hitTarget.transform.parent?.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.DamageTaken(Damage);
            }
        }

        #endregion
    }
}