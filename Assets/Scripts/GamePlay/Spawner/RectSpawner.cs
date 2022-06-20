using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class RectSpawner : MonoBehaviour, ISpawnerObject
    {
        #region Private serialized fields

        [SerializeField] private Bounds spawnBounds;

        #endregion

        #region LifeCycle Method

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 tl = new Vector3(spawnBounds.min.x, spawnBounds.min.y) + transform.position;
            Vector3 tr = new Vector3(spawnBounds.max.x, spawnBounds.min.y) + transform.position;
            Vector3 bl = new Vector3(spawnBounds.min.x, spawnBounds.max.y) + transform.position;
            Vector3 br = new Vector3(spawnBounds.max.x, spawnBounds.max.y) + transform.position;
            Gizmos.DrawLine(tl, tr);
            Gizmos.DrawLine(tr, br);
            Gizmos.DrawLine(br, bl);
            Gizmos.DrawLine(bl, tl);
        }

        #endregion

        #region Interface Method

        public Vector3 GetSpawnPoint()
        {
            float randX = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
            float randY = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
            return transform.position + new Vector3(randX, randY, 0);
        }

        #endregion
    }
}
