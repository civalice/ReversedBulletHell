using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour, ITarget
    {
        #region Property fields

        public Vector3 Direction { get; private set; }
        public float CurrentHp => currentHealth;
        public float MaxHp => MaxHealth;
        public bool IsDead => CurrentHp <= 0;

        #endregion

        [SerializeField] public BaseWeapon HolmingWeapon;
        [SerializeField] public BaseWeapon DiagonalWeapon;
        [SerializeField] public BaseWeapon ParabolaWeapon;

        #region Protect serialized fields

        [SerializeField] protected float MaxHealth;

        #endregion

        #region Private serialized fields

        [SerializeField] private float speed = 30.0f;

        #endregion

        #region Private nonserialized fields

        private Animator animator;
        private Rigidbody2D rigidBody;
        private float currentHealth;
        
        private 

        #endregion

        #region LifeCycle Method

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            Direction = Vector3.right;
        }
        // Start is called before the first frame update
        void Start()
        {
            HolmingWeapon.SetOwner(this);
            DiagonalWeapon.SetOwner(this);
            ParabolaWeapon.SetOwner(this);
        }

        // Update is called once per frame
        void Update()
        {
            InputUpdate();
        }

        #endregion

        #region Public Method

        public void ResetPlayer()
        {
            currentHealth = MaxHealth;
            HolmingWeapon.ResetWeapon();
            DiagonalWeapon.ResetWeapon();
            ParabolaWeapon.ResetWeapon();
        }

        #endregion

        #region Private Method

        private void InputUpdate()
        {
            float distanceDelta = Time.deltaTime * speed;
            Vector3 targetDirection = Vector3.zero;
            bool left = false;
            bool right = false;
            bool down = false;
            bool up = false;
            if (Input.GetKey(KeyCode.W))
            {
                targetDirection += Vector3.up;
                up = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                targetDirection += Vector3.down;
                down = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                targetDirection += Vector3.left;
                left = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                targetDirection += Vector3.right;
                right = true;
            }

            int direction = 0;

            if (!left && !right && down && !up)
            {
                direction = 0;
            }
            else if (right && down && !left && !up)
            {
                direction = 1;
            }
            else if (right && !down && !left && !up)
            {
                direction = 2;
            }
            else if (right && !down && !left && up)
            {
                direction = 3;
            }
            else if (!right && !down && !left && up)
            {
                direction = 4;
            }
            else if (!right && !down && left && up)
            {
                direction = 5;
            }
            else if (!right && !down && left && !up)
            {
                direction = 6;
            }
            else if (!right && down && left && !up)
            {
                direction = 7;
            }

            animator.SetInteger("Direction", direction);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + targetDirection.normalized * speed, distanceDelta);

            //rigidBody.velocity = targetDirection.normalized * distanceDelta;
            bool isWalk = targetDirection.magnitude > 0;
            animator.SetBool("Walk", isWalk);
            if (isWalk)
            {
                Direction = targetDirection.normalized;
            }
        }

        private void Death()
        {
            //Todo: Kill Player
        }

        #endregion

        #region ITarget Implementation

        public T GetTargetComponent<T>()
        {
            return GetComponent<T>();
        }

        public void ModifyStat(BaseStatusEffect statusEffect)
        {
            //Do nothing since we not have any statusEffect for player.
        }

        public void DamageTaken(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Death();
            }
        }

        public void AddHitEffect(List<BaseHitEffect> hitEffects, Vector3 direction)
        {
            //Do nothing for player because no enemy cause any HitEffect yet.
        }

        public Transform GetTargetTransform()
        {
            return transform;
        }

        public void UpgradeWeapon(UpgradeInfo upgrade)
        {
            switch (upgrade.Option)
            {
                case UpgradeOption.Enable:
                {
                    upgrade.Weapon.IsAvailable = true;
                }
                    break;
                case UpgradeOption.AddBurnEffect:
                {
                    upgrade.Weapon.AddHitEffect(new BurnHitEffect());
                }
                    break;
                case UpgradeOption.AddFreezeEffect:
                {
                    upgrade.Weapon.AddHitEffect(new FreezeHitEffect());
                }
                    break;
                case UpgradeOption.AddKnockEffect:
                {
                    upgrade.Weapon.AddHitEffect(new KnockBackHitEffect());
                }
                    break;
                case UpgradeOption.AddDamageModifier:
                {
                    upgrade.Weapon.AddModifier(new DamageModifier());
                }
                    break;
                case UpgradeOption.AddFireRateModifier:
                {
                    upgrade.Weapon.AddModifier(new FireRateModifier());
                }
                    break;
                case UpgradeOption.AddProjectSpeedModifier:
                {
                    upgrade.Weapon.AddModifier(new ProjectileSpeedModifier());
                }
                    break;
                case UpgradeOption.AddPieceModifier:
                {
                    upgrade.Weapon.AddModifier(new PieceModifier());
                }
                    break;
                case UpgradeOption.AddAoEModifier:
                {
                    upgrade.Weapon.AddModifier(new AoEModifier());
                }
                    break;
                case UpgradeOption.AddSubSpawnModifier:
                {
                    upgrade.Weapon.AddModifier(new SubBulletModifier());
                }
                    break;
            }
            upgrade.Weapon.UpgradeList.Add(upgrade.Option);
        }

        #endregion
    }
}