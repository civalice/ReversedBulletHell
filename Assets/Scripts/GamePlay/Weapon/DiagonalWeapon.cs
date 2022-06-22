using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class DiagonalWeapon : BaseWeapon
    {
        public override void FireBullet(Vector3 position, Vector3 direction, bool subSpawn = false, List<Transform> ignoreList = null)
        {
            Vector3[] directionList = { Vector3.up, Vector3.left, Vector3.down, Vector3.right };
            foreach (var dir in directionList)
            {
                var bullet = CreateBullet(position, subSpawn, ignoreList);
                bullet.SetDirection(dir);
            }
        }
    }
}