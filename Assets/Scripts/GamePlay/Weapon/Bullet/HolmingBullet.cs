using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class HolmingBullet : BaseBullet, IDamageDealer
    {
        protected Transform Target;

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (Target == null)
            {
                Target = SpawnSystem.Instance.GetNearestEnemy(transform.position, PiecingList).transform;
            }

            PreviousFramePosition = transform.position;
            transform.position += TargetDirection.normalized * ProjectileSpeed * Time.deltaTime;
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
                        //Add to hit list
                        PiecingList.Add(hit.transform);
                        PlayHitEffect(hit.point);
                        DamageSystem.Instance.DamagingTarget(this, hit.transform);
                        if (PiecingList.Count >= PiecingCount)
                            Destroy(gameObject);
                    }
                }
            }

            if (Target != null)
               TargetDirection = Target.position - transform.position;
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, BulletSize);
        }

        public override void SetDirection(Vector3 targetDirection)
        {
            base.SetDirection(targetDirection);
            Target = SpawnSystem.Instance.GetNearestEnemy(transform.position).transform;
        }

        public float DealDamage()
        {
            return Damage;
        }
    }
}