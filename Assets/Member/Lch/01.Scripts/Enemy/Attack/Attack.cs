using System;
using Member.Ysc._01_Code.Combat.Bullet;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [field: SerializeField] public BaseBullet bulletPrefab { get; protected set; }
    [field: SerializeField] public Transform[] FirePos { get; protected set; }
    
    public abstract void EnemyAttack(Transform target);

    protected virtual void Awake()
    {
        Init();
        AfterInit();
    }

    private void Init()
    {
        if (FirePos == null)
        {
            Debug.LogWarning("FirePos is not found");
        }
    }

    protected virtual void AfterInit()
    {
        // 얘는 재정의 그대로 쓸거에요 :>
    }

    protected void SpawnBullet(Transform target)
    {
        for (int i = 0; i < bulletPrefab.GetBulletCount; i++)
        {
            bulletPrefab = Instantiate(bulletPrefab,FirePos[i].position,Quaternion.identity);
            bulletPrefab.SetDirection(target.position);
        }
    }

}
