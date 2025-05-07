using Member.Ysc._01_Code.Combat.Bullet;
using System.Collections;
using UnityEngine;

public class PlayerAttackCompo : MonoBehaviour,IEntityComponent
{
    private Player _player;

    private bool isAttack;

    private MuzzleSetting[] muzzle;

    [SerializeField]
    private float fireRate = 1f;
    private float fireTimer = 0.8f;

    [field: SerializeField] public float AttackSpeed { get; set; } = 1f;

    [SerializeField]
    private BaseBullet _bullet;

    private EntityVFX entityVFX;
    private readonly string vfxName = "ShootVFX";

    private bool isAutoAim;
    private AutoAimCompo aimCompo;

    private Coroutine coroutine;

    private bool isShootDelay;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _player.InputReader.OnAttackEvent += Debug;
        _player.InputReader.OnAutoAimEvent += IsAutoAim;
        entityVFX = entity.GetCompo<EntityVFX>();
        aimCompo = entity.GetCompo<AutoAimCompo>();
        muzzle = GetComponentsInChildren<MuzzleSetting>();
    }

    private void OnDestroy()
    {
        _player.InputReader.OnAttackEvent -= Debug;
        _player.InputReader.OnAutoAimEvent -= IsAutoAim;
    }

    private void Update()
    {
        if(isAttack && !isShootDelay)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= AttackSpeed / fireRate)
            {
                coroutine = StartCoroutine(FireBullet());
                fireTimer = 0f;
            }
        }
    }
    private Vector3 FireTarget(bool isAuto)
    {
        if (isAuto)
            return aimCompo.target.transform.position;
        else
            return _player.InputReader.GetWorldPosition(out RaycastHit hitInfo);
    }
    private IEnumerator FireBullet()
    {
        isShootDelay = true;
        Vector3 firePoint = FireTarget(isAutoAim);

        for(int i =0; i< muzzle.Length; i++)
        {
            yield return new WaitForSeconds(muzzle[i].shootDelay/AttackSpeed);
            BaseBullet bullet = PoolManager.Instance.Pop(_bullet.name) as BaseBullet;
            bullet.transform.position = muzzle[i].transform.position;
            entityVFX.PlayVfx(vfxName, muzzle[i].transform.position, Quaternion.identity);
            bullet.SetDirection(firePoint);
        }
        isShootDelay = false;
    }

    void IsAutoAim(bool click)
        => isAutoAim = click;

    private void Debug(bool isClick)
    {
        isAttack = isClick;
    }


}
