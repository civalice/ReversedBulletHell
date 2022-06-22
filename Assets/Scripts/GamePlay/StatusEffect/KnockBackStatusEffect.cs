using UnityEngine;

namespace Urxxx.GamePlay
{
    public class KnockBackStatusEffect : BaseStatusEffect
    {
        protected float Force = 0f;
        protected Vector3 Direction;
        private bool isPush = false;
        public void Setup(float force, Vector3 direction)
        {
            Force = force;
            Direction = direction;
        }

        public override void UpdateEffect()
        {
            if (IsComplete()) return;
            if (!isPush)
            {
                var rigidbody = Owner.GetTargetComponent<Rigidbody2D>();
                if (rigidbody != null)
                {
                    rigidbody.AddForce(Direction * Force, ForceMode2D.Impulse);
                }

                isPush = true;
            }
        }

        public override bool IsComplete()
        {
            return isPush;
        }
    }
}
