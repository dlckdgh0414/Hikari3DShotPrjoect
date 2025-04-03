using System;
using System.Collections;
using Member.Ysc._01_Code.Combat.Bullet;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [field: SerializeField] public BaseBullet bulletPrefab { get; protected set; }
    [field: SerializeField] public Transform[] FirePos { get; protected set; }

    public bool IsAttackEnd { get; private set; }

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

        if(bulletPrefab.GetBulletCount <= 1)
        {
            bulletPrefab = Instantiate(bulletPrefab, FirePos[0].position, Quaternion.identity);
            bulletPrefab.SetDirection(target.position);
        }
        else
        {
            StartCoroutine(ManyBulletAttack(timer,target));
        }

       
    }

    private IEnumerator ManyBulletAttack(float timer,Transform target)
    {
        bulletPrefab = Instantiate(bulletPrefab, FirePos[_shotCount].position, Quaternion.identity);
        bulletPrefab.SetDirection(target.position);
        yield return new WaitForSeconds(timer);
        _shotCount++;
        if(_shotCount == FirePos.Length)
        {
            _shotCount = 0;
            IsAttackEnd = true;
        }
    }
}
