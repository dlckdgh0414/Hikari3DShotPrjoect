using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Combat.Bullet;
using Member.Ysc._01_Code.StatSystems;
using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackCompo : MonoBehaviour,IEntityComponent,IAfterInit
{
    [SerializeField]
    private BaseBullet _bullet;
    private Player _player;

    private bool isAttack;

    private MuzzleSetting[] muzzle;

    [SerializeField]
    private float fireRate = 1f;
    private float fireTimer = 0.8f;

    private EntityVFX entityVFX;
    private readonly string vfxName = "ShootVFX";

    private bool isAutoAim;
    private AutoAimCompo aimCompo;

    private Coroutine coroutine;

    private bool isShootDelay;

    [SerializeField]
    private StatSO attackSpeedStat;

    private EntityStat _statCompo;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _player.InputReader.OnAttackEvent += Debug;
        _player.InputReader.OnAutoAimEvent += IsAutoAim;
        entityVFX = entity.GetCompo<EntityVFX>();
        aimCompo = entity.GetCompo<AutoAimCompo>();
        muzzle = GetComponentsInChildren<MuzzleSetting>();
        _statCompo ??= entity.GetCompo<EntityStat>();
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
            if (fireTimer >= attackSpeedStat.Value / fireRate)
            {
                coroutine = StartCoroutine(FireBullet());
                fireTimer = 0f;
            }
        }
    }
    private Vector3 FireTarget(bool isAuto)
    {
        if (isAuto && aimCompo.target != null)
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
            yield return new WaitForSeconds(muzzle[i].shootDelay* attackSpeedStat.Value);
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

    public void AfterInit()
    {
        attackSpeedStat = _statCompo.GetStat(attackSpeedStat);
    }
}
