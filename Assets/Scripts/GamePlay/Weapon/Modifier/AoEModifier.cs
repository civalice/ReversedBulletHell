using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class AoEModifier : BaseModifier
    {
        public float AoERadius = 0.1f;

        public AoEModifier()
        {
            StatModifier.AoERadius = AoERadius;
        }
    }
}