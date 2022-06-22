namespace Urxxx.GamePlay
{
    public class DamageModifier : BaseModifier
    {
        public float BonusDamage = 2f;

        public DamageModifier()
        {
            StatModifier.Damage = BonusDamage;
        }
    }
}