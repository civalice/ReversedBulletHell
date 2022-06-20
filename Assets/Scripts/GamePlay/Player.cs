using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        #region Private serialized fields

        [SerializeField] private float speed = 1.0f;

        #endregion

        #region Private nonserialized fields

        private Rigidbody2D rigidBody;

        #endregion

        #region LifeCycle Method

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        void FixedUpdate()
        {
            InputUpdate();
        }

        #endregion

        #region Public Method

        #endregion

        #region Private Method

        private void InputUpdate()
        {
            float distanceDelta = Time.fixedDeltaTime * speed;
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

            rigidBody.velocity = targetDirection.normalized * distanceDelta;
        }

        #endregion
    }
}