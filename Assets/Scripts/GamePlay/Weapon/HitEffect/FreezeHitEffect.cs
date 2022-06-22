using UnityEngine;

namespace Urxxx.GamePlay
{
    public class FreezeHitEffect : BaseHitEffect
    {
        #region Protect nonserialized fields

        protected float SlowRate = 0.5f;
        protected float Duration = 5f;

        #endregion

        #region Public Method

        public override BaseStatusEffect CreateStatusEffect(Vector3 direction)
        {
            var freezeStatusEffect = new FreezeStatusEffect();
            freezeStatusEffect.Setup(SlowRate, Duration);
            return freezeStatusEffect;
        }

        #endregion
    }
}
