using UnityEngine;

namespace Urxxx.GamePlay
{
    public class HolmingBullet : BaseBullet
    {
        #region Protect nonserialized fields

        protected Transform Target;

        #endregion

        #region LifeCycle Method

        // Update is called once per frame
        protected override void Update()
        {
            if (GameController.Instance.IsPause) return;
            base.Update();

            if (PiecingList.Contains(Target)) Target = null;

            if (Target == null)
            {
                Target = SpawnSystem.Instance.GetNearestEnemy(transform.position, PiecingList)?.transform;
            }

            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }

            PreviousFramePosition = transform.position;
            transform.position += TargetDirection.normalized * ProjectileSpeed * Time.deltaTime;

            if ((Target.position - transform.position).magnitude < 0.1f)
            {
                HitTarget(Target, TargetDirection.normalized);
                PlayHitEffect(Target.position);
            }

            if (Target != null)
            {
                TargetDirection = Target.position - transform.position;
                if (PiecingList.Contains(Target)) Target = null;
            }
        }

        protected void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, BulletSize);
        }

        #endregion

        #region Public Method
        public override void SetDirection(Vector3 targetDirection)
        {
            base.SetDirection(targetDirection);
            Target = SpawnSystem.Instance.GetNearestEnemy(transform.position)?.transform;
        }

        #endregion
    }
}