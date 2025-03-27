using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] private BulletSettingSO BulletSO;

    protected float BulletSpeed = 0;
    protected int BulletCount = 0;
    protected MeshFilter BulletMeshFilter = null;
    public abstract void EnemyAttack(Transform target);

    protected virtual void Awake()
    {
        Init();
        AfterInit();
    }

    private void Init()
    {
        BulletSpeed = BulletSO.BulletSpeed;
        BulletCount = BulletSO.BulletCount;
        BulletMeshFilter = BulletSO.BulletFilter;
    }

    protected virtual void AfterInit()
    {

    }

}
