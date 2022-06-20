using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urxxx.GamePlay;

namespace Urxxx.Testing
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SimpleEnemy : MonoBehaviour
    {
        public GameObject Target;
        public float Speed = 0.3f;
        public Rigidbody2D RigidBody;

        void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
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
            UpdateTarget();
            MoveTowardTarget();


        }


        void UpdateTarget()
        {
            if (Target == null)
            {
                Target = GameController.Instance.GetCurrentPlayer();
            }
        }

        void MoveTowardTarget()
        {
            if (Target == null) return;
            Vector3 targetDirection = Target.transform.position - transform.position;
            RigidBody.velocity = Time.fixedDeltaTime * targetDirection.normalized * Speed;
        }
    }
}