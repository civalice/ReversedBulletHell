using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Urxxx.GamePlay
{
    [Serializable]
    public struct SpawnData
    {
        public GameObject Prefabs;
        public float SpawnRate;
    }

    public class SpawnSystem : MonoBehaviour
    {
        public static SpawnSystem Instance;

        #region Protect serializable fields

        [SerializeField] protected List<GameObject> SpawnerObjectList;
        [SerializeField] protected List<SpawnData> SpawnDataList;
        [SerializeField] protected int StartSpawnCount = 20;

        #endregion

        #region Private nonserializable fields

        private bool isStart = false;
        private int currentSpawnCount;
        private int currentLevel = 1;
        private List<ISpawnerObject> spawnerAreaList = new List<ISpawnerObject>();
        private List<BaseEnemy> enemyList = new List<BaseEnemy>();

        #endregion

        #region LifeCycle Method

        void Awake()
        {
            if (Instance == null) Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            foreach (var spawner in SpawnerObjectList)
            {
                var component = spawner.GetComponent<ISpawnerObject>();
                if (component != null) spawnerAreaList.Add(component);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (GameController.Instance.IsPause) return;
            if (!isStart) return;
            while (enemyList.Count < currentSpawnCount)
            {
                //Get SpawnData
                var spawnData = SpawnDataList.RandomItem();
                Vector3 spawnPoint = GetSpawnPosition();
                var gObj = GameObject.Instantiate(spawnData.Prefabs, spawnPoint, Quaternion.identity, transform);
                var enemy = gObj.GetComponent<BaseEnemy>();
                enemy.Setup(RemoveEnemy);
                enemyList.Add(enemy);
            }
        }

        #endregion

        #region Public Method

        public void StartSpawn()
        {
            isStart = true;
        }

        public void StopSpawn()
        {
            isStart = false;
        }

        public void Reset()
        {
            currentSpawnCount = StartSpawnCount;
            currentLevel = 1;
        }

        public void IncreaseLevel()
        {
            currentLevel++;
            //Config level up
            currentSpawnCount += 1;
        }

        public BaseEnemy GetNearestEnemy(Vector3 position, List<Transform> ignoreList = null)
        {
            bool isFirst = true;
            float minRange = -1;
            BaseEnemy result = null;
            foreach (var enemy in enemyList)
            {
                if (ignoreList != null && ignoreList.Contains(enemy.transform)) continue;
                float range = (enemy.transform.position - position).magnitude;
                if (isFirst)
                {
                    minRange = range;
                    result = enemy;
                    isFirst = false;
                    continue;
                }
                else
                {
                    if (minRange > range)
                    {
                        minRange = range;
                        result = enemy;
                    }
                }
            }

            return result;
        }

        public void Clear()
        {
            foreach (var enemy in enemyList)
            {
                enemy.ForceKill();
            }
            enemyList.Clear();
        }

        #endregion

        #region Private Method

        private void RemoveEnemy(BaseEnemy enemy)
        {
            enemyList.Remove(enemy);
        }

        private Vector3 GetSpawnPosition()
        {
            ISpawnerObject item = spawnerAreaList.RandomItem();
            Vector3 spawnPoint = item.GetSpawnPoint();
            spawnPoint.z = 0;
            return spawnPoint;
        }

        #endregion
    }
}