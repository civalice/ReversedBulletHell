namespace Urxxx.GamePlay
{
    public class FireRateModifier : BaseModifier
    {
        public float BonusFireRate = 20;

        public FireRateModifier()
        {
            StatModifier.FireRate = BonusFireRate;
        }
    }
}