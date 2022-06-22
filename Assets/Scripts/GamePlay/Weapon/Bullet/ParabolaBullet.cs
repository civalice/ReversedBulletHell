using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class ParabolaBullet : BaseBullet
    {
        private float yForce = 0;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            yForce -= Time.deltaTime * 5;
            TargetDirection.y = yForce;
            PreviousFramePosition = transform.position;
            transform.position += TargetDirection * Time.deltaTime;
            LayerMask hitLayer = LayerMask.GetMask("Enemy");
            RaycastHit2D[] hitList = GetHitCast(hitLayer);

            foreach (var hit in hitList)
            {
                // If it hits something...
                if (hit.collider != null && !PiecingList.Contains(hit.transform))
                {
                    //calculate collider range
                    if (IsBetweenPreviousFrame(hit.point) || IsPointInsideCollider(hit.collider))
                    {
                        HitTarget(hit.transform, TargetDirection.normalized);
                        PlayHitEffect(hit.point);
                    }
                }
            }
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, BulletSize);
        }
        public override void SetDirection(Vector3 targetDirection)
        {
            base.SetDirection(targetDirection);
            TargetDirection.y = 0;
            if (TargetDirection.x == 0)
            {
                TargetDirection.x = Random.Range(-0.5f, 0.5f);
            }
            else
            {
                TargetDirection.Normalize();
            }
            yForce = 3 * ProjectileSpeed;
        }
    }
}