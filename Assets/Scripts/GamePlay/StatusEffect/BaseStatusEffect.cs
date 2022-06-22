namespace Urxxx.GamePlay
{
    public class StatModifier
    {
        public float SpeedModifier = 1f;

        protected float BaseValue = 1.0f;

        public StatModifier(float baseValue)
        {
            BaseValue = baseValue;
        }
        public void Reset()
        {
            SpeedModifier = BaseValue;
        }
    }
    public abstract class BaseStatusEffect
    {
        protected ITarget Owner;
        protected StatModifier StatMod = new StatModifier(1f);

        public void SetOwner(ITarget target)
        {
            Owner = target;
        }

        public abstract void UpdateEffect();

        public StatModifier GetStatModifier()
        {
            return StatMod;
        }
        public virtual bool IsComplete()
        {
            return true;
        }
    }
}