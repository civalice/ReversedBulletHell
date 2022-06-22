using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public partial class DirectionalBullet : BaseBullet
    {
        // Start is called before the first frame update
        protected override void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

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
                        HitTarget(hit.transform);
                        PlayHitEffect(hit.point);
                    }
                }
            }
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, BulletSize);
        }
    }
}