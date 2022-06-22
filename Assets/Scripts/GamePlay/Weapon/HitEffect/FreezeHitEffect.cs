using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class FreezeHitEffect : BaseHitEffect
    {
        protected float SlowRate = 0.5f;
        protected float Duration = 5f;

        public override BaseStatusEffect CreateStatusEffect(Vector3 direction)
        {
            var freezeStatusEffect = new FreezeStatusEffect();
            freezeStatusEffect.Setup(SlowRate, Duration);
            return freezeStatusEffect;
        }
    }
}
