using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public partial class DirectionalBullet : BaseBullet, IDamageDealer
    {
        [SerializeField] protected float BulletSize = 1.0f;

        private Vector3 previousFramePosition;

        // Start is called before the first frame update
        protected override void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            previousFramePosition = transform.position;
            transform.position += TargetDirection * ProjectileSpeed * Time.fixedDeltaTime;
            LayerMask hitLayer = LayerMask.GetMask("Enemy");
            RaycastHit2D hit = GetHitCast(hitLayer);
            // If it hits something...
            if (hit.collider != null)
            {
                //calculate collider range
                if (IsBetweenPreviousFrame(hit.point) || IsPointInsideCollider(hit.collider))
                {
                    PlayHitEffect(hit.point);
                    Destroy(gameObject);
                    DamageSystem.Instance.DamagingTarget(this, hit.transform);
                }
            }

            //if ((transform.position - BulletStartPosition).magnitude > BulletRange)
            //{
            //    Destroy(gameObject);
            //}
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, BulletSize);
        }

        private RaycastHit2D GetHitCast(LayerMask layer)
        {
            if (BulletSize > 0)
                return Physics2D.CircleCast(previousFramePosition, BulletSize, TargetDirection, ProjectileSpeed * 10,
                    layer);
            else
                return Physics2D.Raycast(previousFramePosition, TargetDirection, ProjectileSpeed * 10, layer);
        }

        private bool IsBetweenPreviousFrame(Vector3 position)
        {
            var previousRange = (transform.position - previousFramePosition).magnitude;
            var testRange = (position - previousFramePosition).magnitude;
            return previousRange >= testRange;
        }

        private bool IsPointInsideCollider(Collider2D collider)
        {
            if (BulletSize > 0)
                return Physics2D.OverlapCircleAll(previousFramePosition, BulletSize).Contains(collider) ||
                       Physics2D.OverlapCircleAll(transform.position, BulletSize).Contains(collider);
            else
                return collider.OverlapPoint(previousFramePosition) || collider.OverlapPoint(transform.position);
        }

        #region IDamageDealer implement

        public float DealDamage()
        {
            return Damage;
        }
        
        #endregion
    }
}
