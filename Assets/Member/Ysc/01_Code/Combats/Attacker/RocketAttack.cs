using Member.Ysc._01_Code.Containers;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Attacker
{
    public class RocketAttack : Attack
    {
        public override void EnemyAttack(Transform target, float timer)
        {
            TargetContainer container = new TargetContainer();
            container.targetPos = target.position; 
            SpawnBullet(container, timer);
        }
    }
}