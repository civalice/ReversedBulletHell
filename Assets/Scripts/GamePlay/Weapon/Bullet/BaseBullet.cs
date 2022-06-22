using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public abstract class BaseBullet : MonoBehaviour, IDamageDealer
    {
        #region Property fields

        public float Damage => Weapon.Damage;
        public float ProjectileSpeed => Weapon.ProjectileSpeed;
        public int PiecingCount => Weapon.PiecingCount;
        public float AoE => Weapon.AoERadius;

        #endregion

        #region Protect serialized fields

        [SerializeField] protected GameObject HitEffectPrefab;
        [SerializeField] protected float BulletSize = 1.0f;
        [SerializeField] protected float BulletRange = 10f;

        #endregion

        #region Protect nonserialized fields

        protected bool SubSpawn = false;
        protected BaseWeapon Weapon;
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
            if (GameController.Instance.IsPause) return;
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

        public virtual void SetupBullet(BaseWeapon weapon, bool subSpawn, List<Transform> ignoreList)
        {
            Weapon = weapon;
            SubSpawn = subSpawn;
            if (ignoreList != null)
                PiecingList.AddRange(ignoreList);
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
                if (AoE > 0)
                {
                    LayerMask hitLayer = LayerMask.GetMask("Enemy");
                    var aoeTargetList = Physics2D.OverlapCircleAll(targetTransform.position, AoE, hitLayer);
                    foreach (var aoeTarget in aoeTargetList)
                    {
                        if (aoeTarget.transform == targetTransform) continue;
                        var targetComponent = aoeTarget.gameObject.GetComponent<ITarget>();
                        if (targetComponent != null)
                        {
                            Vector3 aoeDirection = aoeTarget.transform.position - targetTransform.position;
                            DamageSystem.Instance.DamagingTarget(this, targetComponent);
                            targetComponent.AddHitEffect(HitEffects, aoeDirection.normalized);
                        }
                    }
                }
                DamageSystem.Instance.DamagingTarget(this, target);
                target.AddHitEffect(HitEffects, direction);
                if (SubSpawn)
                {
                    Weapon.FireBullet(targetTransform.position, direction, false, PiecingList);
                    SubSpawn = false;
                }
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