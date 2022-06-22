using UnityEngine;

namespace Urxxx.GamePlay
{
    public class BurnHitEffect : BaseHitEffect
    {
        #region Protect nonserialized fields

        protected float Damage = 1f;
        protected float Duration = 5f;

        #endregion

        #region Public Method

        public override BaseStatusEffect CreateStatusEffect(Vector3 direction)
        {
            var burnStatusEffect = new BurnStatusEffect();
            burnStatusEffect.Setup(Damage, Duration);
            return burnStatusEffect;
        }

        #endregion
    }
}