using UnityEngine;

namespace Urxxx.GamePlay
{
    public abstract class BaseHitEffect
    {
        public abstract BaseStatusEffect CreateStatusEffect(Vector3 direction);
    }
}
