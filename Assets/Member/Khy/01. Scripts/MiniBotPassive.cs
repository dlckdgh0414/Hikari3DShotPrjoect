using Member.Ysc._01_Code.Combat.Bullet;
using MoreMountains.Tools;
using System;
using System.Collections;
using UnityEngine;

public class MiniBotPassive : PassiveSkill
{
    [field: SerializeField]
    private MMAutoRotate miniBot;

    [Header("πÃ¥œ∫ø √—æÀ")]
    [SerializeField]
    private BaseBullet _miniBotBullet;

    private PlayerAttackCompo _attackCompo;

    [Header("πﬂªÁ µÙ∑π¿Ã")]
    private float delay=3f;
    private float time;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        Debug.Log("¿Ã¥œº»");
        _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        miniBot.gameObject.SetActive(true);
    }
    public override void PassiveAbility()
    {
        base.PassiveAbility();
        time += Time.deltaTime;
        if (delay < time)
        {
            Shoot();
            time = 0f;
        }
    }

    private void Shoot()
    {
        Vector3 firePoint = _attackCompo.FireTarget();
        BaseBullet bullet = PoolManager.Instance.Pop(_miniBotBullet.name) as BaseBullet;
        bullet.transform.position = miniBot.transform.position;
        bullet.SetDirection(firePoint);
    }
}
