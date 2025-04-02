using Member.Ysc._01_Code.Combat.Bullet;
using UnityEngine;

namespace Member.Ysc._01_Code.Combat.Attacker
{
    public class NormalAttack : Attack
    {
        public override void EnemyAttack(Transform target)
        {
            Debug.Log("공격"); 
            BaseBullet bullet = Instantiate(bulletPrefab, FirePos.position, Quaternion.identity);
        }
    }
}