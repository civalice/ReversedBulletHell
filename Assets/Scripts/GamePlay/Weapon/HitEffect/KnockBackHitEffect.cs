using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class KnockBackHitEffect : BaseHitEffect
    {
        protected float Force = 2f;

        public override BaseStatusEffect CreateStatusEffect(Vector3 direction)
        {
            var knockBackStatuseffect = new KnockBackStatusEffect();
            knockBackStatuseffect.Setup(Force, direction);
            return knockBackStatuseffect;
        }
    }
}
