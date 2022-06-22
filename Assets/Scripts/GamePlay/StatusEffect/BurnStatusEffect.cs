using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class BurnStatusEffect : BaseStatusEffect, IDamageDealer
    {
        protected float Damage = 1f;
        protected float Duration = 5f;

        private float time = 0;
        private float elapsedTime = 0;

        public void Setup(float damage, float duration)
        {
            Damage = damage;
            Duration = duration;
            elapsedTime = Duration;
            time = 0;
        }

        public override void UpdateEffect()
        {
            if (IsComplete()) return;
            elapsedTime -= Time.deltaTime;
            time += Time.deltaTime;
            if (time >= 1)
            {
                time -= 1;
                DamageSystem.Instance.DamagingTarget(this, Owner);
            }
        }

        public override bool IsComplete()
        {
            return elapsedTime <= 0;
        }

        public float DealDamage()
        {
            return Damage;
        }
    }
}
