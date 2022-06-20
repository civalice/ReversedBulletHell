using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urxxx.System;

namespace Urxxx.GamePlay
{
    public sealed class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        #region Private serialized fields

        [SerializeField] private FollowCamera mainCamera;
        [SerializeField] private Player player;

        #endregion

        #region Private nonserialized fields



        #endregion

        #region LifeCycle Method

        void Awake()
        {
            if (Instance == null) Instance = this;
            SetupBackground();
        }

        // Start is called before the first frame update
        void Start()
        {
            SetupFollowCamera();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public Method

        public GameObject GetCurrentPlayer()
        {
            return player.gameObject;
        }

        #endregion

        #region Private Method

        private void SetupFollowCamera()
        {
            if (mainCamera != null && player != null)
            {
                mainCamera.FollowTarget = player.transform;
            }
        }

        private void SetupBackground()
        {
            if (mainCamera != null)
            {
                EndlessBackground.SetAllTargetCamera(mainCamera.transform);
            }
        }

        #endregion
    }
}