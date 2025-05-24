using System;
using System.Collections;
using Member.Ysc._01_Code.Combat.Bullet;
using Ami.BroAudio;
using Member.Ysc._01_Code.Containers;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Attack : MonoBehaviour
{
    [field: SerializeField] public BaseBullet bulletPrefab { get; protected set; }
    [field: SerializeField] public Transform[] FirePos { get; protected set; }

    [SerializeField] private SoundID enemyAttackSFX;

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

    protected void SpawnBullet(TargetContainer container,float timer, bool? isGuided = null)
    {
        if (bulletPrefab.GetBulletCount <= 1)
        {
            int range = 10;
            bulletPrefab = PoolManager.Instance.Pop(bulletPrefab.name) as BaseBullet;
            bulletPrefab.transform.position = FirePos[0].position;
            bulletPrefab.SetTransform();
            BroAudio.Play(enemyAttackSFX);
            if (isGuided == null)
            {
                range = Random.Range(0, 10);
            }
            
            if (isGuided == true)
            {
                Debug.Log($"가이드 오브젝트 이름 : {transform.parent.name}");
                bulletPrefab.SetTransform(container);
                bulletPrefab.IsPlayerFollow = true;
            } 
            else if (range<= 7)
            {
                Debug.Log($"힣 오브젝트 이름 : {transform.parent.name}");
                bulletPrefab.SetDirection(container.targetPos);
                bulletPrefab.IsPlayerFollow = true;
            }
            else
            {
                bulletPrefab.IsPlayerFollow = false;
            }
        }
        else
        {
            StartCoroutine(ManyBulletAttack(timer,container,isGuided));
        }
        IsAttackEnd = true;
    }

    private IEnumerator ManyBulletAttack(float timer,TargetContainer container, bool? isGuided = null)
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
            bulletPrefab.SetTransform();
            
            BroAudio.Play(enemyAttackSFX);
            int range = 10;
            if (isGuided == null)
            {
                range = Random.Range(0, 10);
            }
            
            if (isGuided == true)
            {
                Debug.Log($"가이드 오브젝트 이름 : {transform.parent.name}");
                bulletPrefab.SetTransform(container);
                bulletPrefab.IsPlayerFollow = true;
            }
            else if (range <= 7)
            {
                Debug.Log($"힣 오브젝트 이름 : {transform.parent.name}");
                bulletPrefab.SetDirection(container.targetPos);
                bulletPrefab.IsPlayerFollow = true;
            }
            else
            {
                bulletPrefab.IsPlayerFollow = false;
            }
            yield return new WaitForSeconds(timer);
            _shotCount++;
        }
       
    }
}
