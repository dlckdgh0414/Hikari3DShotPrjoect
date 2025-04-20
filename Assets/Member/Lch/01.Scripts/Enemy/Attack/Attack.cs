using System;
using System.Collections;
using Member.Ysc._01_Code.Combat.Bullet;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [field: SerializeField] public BaseBullet bulletPrefab { get; protected set; }
    [field: SerializeField] public Transform[] FirePos { get; protected set; }

    public bool IsAttackEnd { get; set; }

    private int _shotCount = 0;
    
    public abstract void EnemyAttack(Transform target,float timer);

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

    protected void SpawnBullet(Transform target,float timer)
    {
        if (bulletPrefab.GetBulletCount <= 1)
        {
            bulletPrefab = PoolManager.Instance.Pop(bulletPrefab.name) as BaseBullet;
            bulletPrefab.transform.position = FirePos[0].position;
            bulletPrefab.SetDirection(target.position);
        }
        else
        {
            StartCoroutine(ManyBulletAttack(timer,target));
        }
    }

    private IEnumerator ManyBulletAttack(float timer,Transform target)
    {
        while (!IsAttackEnd)
        {
            if(_shotCount == FirePos.Length)
            {
                IsAttackEnd = true;
                _shotCount = 0;
            }
            bulletPrefab = PoolManager.Instance.Pop(bulletPrefab.name) as BaseBullet;
            bulletPrefab.transform.position = FirePos[_shotCount].position;
            bulletPrefab.SetDirection(target.position);
            yield return new WaitForSeconds(timer);
            _shotCount++;
        }
       
    }
}
