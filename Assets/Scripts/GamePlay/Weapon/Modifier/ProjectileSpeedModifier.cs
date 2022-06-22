namespace Urxxx.GamePlay
{
    public class ProjectileSpeedModifier : BaseModifier
    {
        public float BonusProjectileSpeed = 0.2f;

        public ProjectileSpeedModifier()
        {
            StatModifier.ProjectileSpeed = BonusProjectileSpeed;
        }
    }
}