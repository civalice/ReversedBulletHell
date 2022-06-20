using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.System
{
    public class FollowCamera : MonoBehaviour
    {
        #region Public nonserialized fields

        public Transform FollowTarget;

        #endregion

        #region Private serialized fields

        [SerializeField] private float speed = 1.0f;

        #endregion

        #region LifeCycle Method

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (FollowTarget == null) return;

            var position = transform.position;
            position = Vector3.Lerp(position, FollowTarget.position, Time.deltaTime * speed);
            position.z = transform.position.z;
            transform.position = position;
        }

        #endregion
    }
}