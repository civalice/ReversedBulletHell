using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour, ITarget
    {
        #region Property fields

        public Vector3 Direction { get; private set; }
        public float CurrentHealth => currentHealth;
        public bool IsDead => CurrentHealth <= 0;

        #endregion

        #region Protect serialized fields

        [SerializeField] protected float MaxHealth;

        #endregion

        #region Private serialized fields

        [SerializeField] private float speed = 30.0f;

        #endregion

        #region Private nonserialized fields

        private Rigidbody2D rigidBody;
        private float currentHealth;
        
        private 

        #endregion

        #region LifeCycle Method

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            Direction = Vector3.right;
        }
        // Start is called before the first frame update
        void Start()
        {

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
        }

        #endregion

        #region Private Method

        private void InputUpdate()
        {
            float distanceDelta = Time.deltaTime * speed;
            Vector3 targetDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                targetDirection += Vector3.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                targetDirection += Vector3.down;
            }

            if (Input.GetKey(KeyCode.A))
            {
                targetDirection += Vector3.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                targetDirection += Vector3.right;
            }

            transform.position = Vector3.MoveTowards(transform.position, transform.position + targetDirection.normalized * speed, distanceDelta);

            //rigidBody.velocity = targetDirection.normalized * distanceDelta;
            if (targetDirection.magnitude > 0)
                Direction = targetDirection.normalized;
        }

        private void Death()
        {
            //Todo: Kill Player
        }

        #endregion

        #region ITarget Implementation

        public void DamageTaken(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Death();
            }
        }

        #endregion
    }
}