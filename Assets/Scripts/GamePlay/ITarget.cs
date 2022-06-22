using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{ 
    public interface ITarget
    {
        Transform GetTargetTransform();
        T GetTargetComponent<T>();
        void DamageTaken(float damage);
        void AddHitEffect(List<BaseHitEffect> hitEffects, Vector3 direction);
    }
}