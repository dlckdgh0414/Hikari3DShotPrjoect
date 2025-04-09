using UnityEngine;

public class MissileAttack : Attack
{
    public override void EnemyAttack(Transform target, float timer)
    {
        SpawnBullet(target, timer);
    }
}
