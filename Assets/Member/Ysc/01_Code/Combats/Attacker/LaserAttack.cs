using System;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Attacker
{
    public class LaserAttack : Attack
    {
        public override void EnemyAttack(Transform target, float timer)
        {
            SpawnBullet(target, timer);
        }
    }
}