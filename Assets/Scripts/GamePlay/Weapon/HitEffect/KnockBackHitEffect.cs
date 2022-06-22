using UnityEngine;

namespace Urxxx.GamePlay
{
    public class KnockBackHitEffect : BaseHitEffect
    {
        #region Protect nonserialized fields

        protected float Force = 2f;

        #endregion

        #region Public Method
        public override BaseStatusEffect CreateStatusEffect(Vector3 direction)
        {
            var knockBackStatuseffect = new KnockBackStatusEffect();
            knockBackStatuseffect.Setup(Force, direction);
            return knockBackStatuseffect;
        }
        
        #endregion
    }
}
