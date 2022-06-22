using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Urxxx.System;
using Utilities;

namespace Urxxx.GamePlay
{
    public enum GameState
    {
        Start,
        Upgrade,
        GamePlay,
        GameOver
    }

    public sealed class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        #region Property fields

        public bool IsStart => state == GameState.GamePlay || state == GameState.Upgrade;
        public bool IsPause => state == GameState.Upgrade;

        #endregion

        #region Private serialized fields

        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private FollowCamera mainCamera;
        [SerializeField] private GamePlayUI gameUi;
        [SerializeField] private UpgradeUI upgradeUi;

        #endregion

        #region Private nonserialized fields

        private GameState state = GameState.Start;
        private Player player;
        private int currentExperience = 0;
        private int nextLevelExperience = 100;

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
            MenuInput();
            switch (state)
            {
                case GameState.Start:
                {
                }
                    break;
                case GameState.Upgrade:
                {
                    Time.timeScale = 0;
                }
                    break;
                case GameState.GamePlay:
                {
                    Time.timeScale = 1;
                    gameUi.SetHp(player);

                    if (player.IsDead)
                    {
                        GameOver();
                    }
                }
                    break;
                case GameState.GameOver:
                {
                }
                    break;
            }
        }

        #endregion


        #region Public Method

        public GameObject GetCurrentPlayer()
        {
            return player.gameObject;
        }

        public void ShowUpgrade()
        {
            var upgradeList = UpgradeHelper.GetUpgradeList(player);
            upgradeList.RandomRemove(3);
            upgradeUi.SetupUpgrade(upgradeList);
        }

        public void ChooseUpgrade(UpgradeInfo upgrade)
        {
            player.UpgradeWeapon(upgrade);
            state = GameState.GamePlay;
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

        private void MenuInput()
        {
            if (!IsStart)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartGame();
                }
            }
        }
        private void StartGame()
        {
            GameObject playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player = playerObject.GetComponent<Player>();
            SetupFollowCamera();
            gameUi.HideStartText();
            player.ResetPlayer();
            //SkillText.text = Player.GetSkillListName();
            SpawnSystem.Instance.Reset();
            SpawnSystem.Instance.StartSpawn();
            state = GameState.Upgrade;
            ShowUpgrade();
        }

        private void GameOver()
        {
            SpawnSystem.Instance.Clear();
            SpawnSystem.Instance.StopSpawn();
            gameUi.SetStartText("GameOver\n Press Space to continue...");
            state = GameState.GameOver;
        }

        #endregion
    }
}