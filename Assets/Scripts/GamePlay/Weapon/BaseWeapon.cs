using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class BaseWeapon : MonoBehaviour
    {
        #region Protect serialized fields

        [SerializeField] protected GameObject BulletPrefab;
        [SerializeField] protected int PiecingCount = 1;
        [SerializeField] protected float FireRate = 1.0f;
        [SerializeField] protected float Damage = 1.0f;
        [SerializeField] protected float ProjectileSpeed = 3.0f;
        [SerializeField] protected Player Owner;

        #endregion

        #region Protect nonserialized fields

        protected List<BaseHitEffect> HitEffectList = new List<BaseHitEffect>();

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
                FireBullet(Owner.Direction);
            }
        }

        #endregion

        #region Public Method

        public void SetOwner(Player player)
        {
            Owner = player;
        }

        #endregion

        #region Protect Method

        protected virtual BaseBullet CreateBullet()
        {
            var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            var bulletComponent = bullet.GetComponent<BaseBullet>();
            bulletComponent.SetupBullet(Damage, ProjectileSpeed, PiecingCount);
            foreach (var hitEffect in HitEffectList)
            {
                bulletComponent.AddHitEffect(hitEffect);
            }
            return bulletComponent;
        }

        protected virtual void FireBullet(Vector3 direction)
        {
            var bullet = CreateBullet();
            bullet.SetDirection(direction);
        }

        #endregion
    }
}