using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Combat.Bullet;
using Member.Ysc._01_Code.StatSystems;
using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackCompo : MonoBehaviour, IEntityComponent, IAfterInit
{
    [SerializeField]
    private BaseBullet _defalutBullet;
    [SerializeField]
    private BaseBullet _chargeBullet;
    private Entity _entity;
    private Player _player;

    private bool isAttack;

    private MuzzleSetting[] muzzle;

    [field : SerializeField]
    public float FireRate { get; set; } = 3f;
    private float fireTimer = 0.8f;

    private EntityVFX entityVFX;
    private readonly string vfxName = "ShootVFX";

    private AutoAimCompo aimCompo;

    private Coroutine coroutine;

    private bool isShootDelay;

    [SerializeField]
    private StatSO attackSpeedStat;

    private EntityStat _statCompo;

    public event Action OnAttack;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _player = entity as Player;
        _player.InputReader.OnAttackEvent += AttackBool;
        entityVFX = entity.GetCompo<EntityVFX>();
        aimCompo = entity.GetCompo<AutoAimCompo>();
        muzzle = GetComponentsInChildren<MuzzleSetting>();
        _statCompo ??= entity.GetCompo<EntityStat>();
    }
    public void AfterInit()
    {
        attackSpeedStat = _statCompo.GetStat(attackSpeedStat);
    }

    private void OnDestroy()
    {
        _player.InputReader.OnAttackEvent -= AttackBool;
    }

    private void Update()
    {
        if (_player.IsDead || !Entity.IsGameStart) return;
        if(isAttack && !isShootDelay)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= attackSpeedStat.Value / FireRate)
            {
                coroutine = StartCoroutine(FireBullet());
                fireTimer = 0f;
            }
        }
    }
    public Vector3 FireTarget()
    {
        if (aimCompo.target != null && aimCompo.IsAutoAim)
            return aimCompo.target.transform.position;
        else
            return _player.InputReader.GetWorldPosition(out RaycastHit hitInfo);
    }
    private IEnumerator FireBullet()
    {
        isShootDelay = true;
        Vector3 firePoint = FireTarget();

        for(int i =0; i< muzzle.Length; i++)
        {
            yield return new WaitForSeconds(muzzle[i].shootDelay* attackSpeedStat.Value);
            BaseBullet bullet = PoolManager.Instance.Pop(_defalutBullet.name) as BaseBullet;
            bullet.transform.position = muzzle[i].transform.position;
            entityVFX.PlayVfx(vfxName, muzzle[i].transform.position, Quaternion.identity);
            bullet.SetDirection(firePoint);
        }
        isShootDelay = false;
        OnAttack?.Invoke();
    }

    private void AttackBool(bool isClick)
    {
        isAttack = isClick;
    }


    public void ChargeShoot()
    {
        FireChargeBullet();
    }

    private void FireChargeBullet()
    {
        Vector3 firePoint = FireTarget();
        isShootDelay = true;
        for (int i = 0; i < muzzle.Length; i++)
        {
            BaseBullet bullet = PoolManager.Instance.Pop(_chargeBullet.name) as BaseBullet;
            bullet.transform.position = muzzle[i].transform.position;
            entityVFX.PlayVfx(vfxName, muzzle[i].transform.position, Quaternion.identity);
            bullet.SetDirection(firePoint);
        }
        isShootDelay = false;
    }
}
