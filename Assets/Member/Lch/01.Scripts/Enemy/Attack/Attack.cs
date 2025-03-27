using Member.Ysc._01_Code.Agent.Enemy.Combat.Bullet;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] private BaseBullet Bullet;
    
    public abstract void EnemyAttack(Transform target);

    protected virtual void Awake()
    {
        Init();
        AfterInit();
    }

    private void Init()
    {
        // 일단 혹시 모르니 비워둘게용
    }

    protected virtual void AfterInit()
    {
        // 얘는 재정의 그대로 쓸거에요 :>
    }

}
