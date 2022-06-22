using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class FreezeStatusEffect : BaseStatusEffect
    {
        protected float SlowRate = 0.5f;
        protected float Duration = 3f;

        private float elapsedTime = 0;

        public void Setup(float slowRate, float duration)
        {
            SlowRate = slowRate;
            Duration = duration;
            elapsedTime = Duration;
        }

        public override void UpdateEffect()
        {
            if (IsComplete()) return;
            elapsedTime -= Time.deltaTime;
            var enemy = Owner.GetTargetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.ModifyStat(this);
            }
        }

        public override bool IsComplete()
        {
            return elapsedTime <= 0;
        }
    }
}
