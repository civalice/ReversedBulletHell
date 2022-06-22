using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class BaseWeapon : MonoBehaviour
    {
        #region Property fields

        [HideInInspector] public int PiecingCount => BasePiecingCount + BaseStatModifier.PieceCount;
        [HideInInspector] public float FireRate => 100f / (BaseFireRate + BaseStatModifier.FireRate);
        [HideInInspector] public float Damage => BaseDamage + BaseStatModifier.Damage;
        [HideInInspector] public float ProjectileSpeed => BaseProjectileSpeed + BaseStatModifier.ProjectileSpeed;
        [HideInInspector] public float AoERadius => BaseAoERadius + BaseStatModifier.AoERadius;
        [HideInInspector] public bool SubSpawn => BaseStatModifier.SubSpawn;
        #endregion

        #region Protect serialized fields

        [SerializeField] protected GameObject BulletPrefab;
        [SerializeField] protected int BasePiecingCount = 1;
        [SerializeField] protected float BaseFireRate = 100f;
        [SerializeField] protected float BaseDamage = 1.0f;
        [SerializeField] protected float BaseProjectileSpeed = 3.0f;
        [SerializeField] protected Player Owner;

        #endregion

        #region Protect nonserialized fields

        protected float BaseAoERadius = 0f;
        protected WeaponStatModifier BaseStatModifier = new WeaponStatModifier();
        protected List<BaseHitEffect> HitEffectList = new List<BaseHitEffect>();
        protected List<BaseModifier> ModifierList = new List<BaseModifier>();

        #endregion

        #region Private nonserialized fields

        private float timing = 0;

        #endregion

        #region LifeCycle Method

        protected virtual void Awake()
        {
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            timing += Time.deltaTime;
            while (timing > FireRate)
            {
                timing -= FireRate;
                FireBullet(Owner.GetTargetTransform().position, Owner.Direction, SubSpawn);
            }
        }

        #endregion

        #region Public Method

        public void SetOwner(Player player)
        {
            Owner = player;
        }
        public virtual void FireBullet(Vector3 position, Vector3 direction, bool subSpawn = false, List<Transform> ignoreList = null)
        {
            var bullet = CreateBullet(position, subSpawn, ignoreList);
            bullet.SetDirection(direction);
        }
        public virtual void AddHitEffect(BaseHitEffect hitEffect)
        {
            HitEffectList.Add(hitEffect);
        }
        public virtual void AddModifier(BaseModifier weaponModifier)
        {
            if (weaponModifier == null) return;
            ModifierList.Add(weaponModifier);
            RecalculateStatModifier();
        }
        #endregion

        #region Protect Method

        protected virtual void RecalculateStatModifier()
        {
            BaseStatModifier.Reset();
            foreach (var modifier in ModifierList)
            {
                var stat = modifier.GetStatModifier();
                BaseStatModifier.Damage += stat.Damage;
                BaseStatModifier.FireRate += stat.FireRate;
                BaseStatModifier.ProjectileSpeed += stat.ProjectileSpeed;
                BaseStatModifier.AoERadius += stat.AoERadius;
                BaseStatModifier.PieceCount += stat.PieceCount;
                BaseStatModifier.SubSpawn |= stat.SubSpawn;
            }
        }

        protected virtual BaseBullet CreateBullet(Vector3 position, bool subSpawn, List<Transform> ignoreList)
        {
            var bullet = Instantiate(BulletPrefab, position, Quaternion.identity);
            var bulletComponent = bullet.GetComponent<BaseBullet>();
            bulletComponent.SetupBullet(this, subSpawn, ignoreList);
            foreach (var hitEffect in HitEffectList)
            {
                bulletComponent.AddHitEffect(hitEffect);
            }
            return bulletComponent;
        }

        #endregion
    }
}