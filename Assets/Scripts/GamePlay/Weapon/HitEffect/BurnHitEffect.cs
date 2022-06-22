using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class BurnHitEffect : BaseHitEffect
    {
        protected float Damage = 1f;
        protected float Duration = 5f;

        public override BaseStatusEffect CreateStatusEffect()
        {
            var burnStatusEffect = new BurnStatusEffect();
            burnStatusEffect.Setup(Damage, Duration);
            return burnStatusEffect;
        }
    }
}