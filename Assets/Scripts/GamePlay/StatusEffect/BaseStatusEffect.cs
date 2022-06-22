using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public abstract class BaseStatusEffect
    {
        protected ITarget Owner;

        public void SetOwner(ITarget target)
        {
            Owner = target;
        }

        public abstract void UpdateEffect();
        
        public virtual bool IsComplete()
        {
            return true;
        }
    }
}