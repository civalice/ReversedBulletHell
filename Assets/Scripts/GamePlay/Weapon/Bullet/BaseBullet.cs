using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public abstract class BaseBullet : MonoBehaviour, IDamageDealer
    {
        #region Protect serialized fields

        [SerializeField] protected GameObject HitEffectPrefab;
        [SerializeField] protected float BulletSize = 1.0f;
        [SerializeField] protected float BulletRange = 10f;
        [SerializeField] protected float Damage = 1.0f;
        [SerializeField] protected float ProjectileSpeed = 3.0f;
        [SerializeField] protected int PiecingCount = 1;

        #endregion

        #region Protect nonserialized fields

        protected Vector3 StartPosition;
        protected Vector3 TargetDirection;
        protected bool IsBulletLaunch = false;
        protected Vector3 PreviousFramePosition;
        protected List<Transform> PiecingList = new List<Transform>();
        protected List<BaseHitEffect> HitEffects = new List<BaseHitEffect>();

        #endregion

        #region LifeCycle Method

        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!IsBulletLaunch) return;
            if (IsComplete()) 
                Destroy(gameObject);
        }

        #endregion

        #region Public Method

        public void AddHitEffect(BaseHitEffect hitEffect)
        {
            HitEffects.Add(hitEffect);
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

        protected virtual void HitTarget(Transform targetTransform, Vector3 direction)
        {
            PiecingList.Add(targetTransform);
            var target = targetTransform.GetComponent<ITarget>();
            if (target != null)
            {
                DamageSystem.Instance.DamagingTarget(this, target);
                target.AddHitEffect(HitEffects, direction);
                if (PiecingList.Count >= PiecingCount)
                    Destroy(gameObject);
            }
        }

        protected virtual void PlayHitEffect(Vector2 hitPosition)
        {
            if (HitEffectPrefab == null) return;
            var randomVal = 0.02f;
            var hitEffect = Instantiate(HitEffectPrefab,
                hitPosition + new Vector2(Random.Range(-randomVal, randomVal),
                    Random.Range(-randomVal, randomVal)), Quaternion.identity);
        }

        protected RaycastHit2D[] GetHitCast(LayerMask layer)
        {
            if (BulletSize > 0)
                return Physics2D.CircleCastAll(PreviousFramePosition, BulletSize, TargetDirection, ProjectileSpeed * 10,
                    layer);
            else
                return Physics2D.RaycastAll(PreviousFramePosition, TargetDirection, ProjectileSpeed * 10, layer);
        }

        protected bool IsBetweenPreviousFrame(Vector3 position)
        {
            var previousRange = (transform.position - PreviousFramePosition).magnitude;
            var testRange = (position - PreviousFramePosition).magnitude;
            return previousRange >= testRange;
        }

        protected bool IsPointInsideCollider(Collider2D collider)
        {
            if (BulletSize > 0)
                return Physics2D.OverlapCircleAll(PreviousFramePosition, BulletSize).Contains(collider) ||
                       Physics2D.OverlapCircleAll(transform.position, BulletSize).Contains(collider);
            else
                return collider.OverlapPoint(PreviousFramePosition) || collider.OverlapPoint(transform.position);
        }


        #endregion

        #region IDamageDealer implement

        public virtual float DealDamage()
        {
            return Damage;
        }

        #endregion
    }
}