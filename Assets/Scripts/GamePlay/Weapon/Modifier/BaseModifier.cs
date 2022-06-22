namespace Urxxx.GamePlay
{
    public class WeaponStatModifier
    {
        public int PieceCount = 0;
        public float FireRate = 0f;
        public float Damage = 0;
        public float ProjectileSpeed = 0;
        public float AoERadius = 0;
        public bool SubSpawn = false;

        public void Reset()
        {
            PieceCount = 0;
            FireRate = 0;
            Damage = 0;
            ProjectileSpeed = 0;
            AoERadius = 0;
            SubSpawn = false;
        }
    }

    public class BaseModifier
    {
        protected WeaponStatModifier StatModifier = new WeaponStatModifier();

        public WeaponStatModifier GetStatModifier()
        {
            return StatModifier;
        }
    }
}