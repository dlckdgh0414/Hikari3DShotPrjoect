using Member.Ysc._01_Code.Containers;
using UnityEngine;

public class MissileAttack : Attack
{
    public override void EnemyAttack(Transform target, float timer)
    {
        TargetContainer container = new TargetContainer();
        container.targetTrm = target; 
        SpawnBullet(container, timer);
    }
}
