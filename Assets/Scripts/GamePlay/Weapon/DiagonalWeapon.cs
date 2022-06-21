using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.GamePlay
{
    public class DiagonalWeapon : BaseWeapon
    {
        protected override void FireBullet(Vector3 direction)
        {
            Vector3[] directionList = { Vector3.up, Vector3.left, Vector3.down, Vector3.right };
            foreach (var dir in directionList)
            {
                var bullet = CreateBullet();
                bullet.SetDirection(dir);
            }
        }
    }
}