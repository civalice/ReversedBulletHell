using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urxxx.GamePlay;

namespace Urxxx.GamePlay
{
    public class FreezeHitEffect : BaseHitEffect
    {
        protected float SlowRate = 1f;
        protected float Duration = 5f;

        public override BaseStatusEffect CreateStatusEffect()
        {
            var freezeStatusEffect = new FreezeStatusEffect();
            freezeStatusEffect.Setup(SlowRate, Duration);
            return freezeStatusEffect;
        }
    }
}
