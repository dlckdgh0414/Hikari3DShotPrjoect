using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class SelfBoomSkill : ActiveSkill
{
    private readonly string fuseEffect = "BombFuse";
    private readonly string explosionEffect = "Explosion";

    [Header("������ �ð�")]
    public float duration;
    [Header("��鸲 ����")]
    public float intensity;
    [Header("���� ������")]
    public float crashDamage= 20f;
    [Header("���� �� ȸ�� �ӵ�")]
    public float healingCrash = 1f;

    [SerializeField]
    private MMF_Player cameraFeel;

    private EntityHealthCompo healthCompo;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        healthCompo = entity.GetCompo<EntityHealthCompo>();
    }

    public override void OverSkillCooltime()
    {
        base.OverSkillCooltime();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        entityVFX.PlayVfx(fuseEffect, Vector3.zero, Quaternion.identity);
        
        StartCoroutine(SlowBulletRoutine());
    }

    private IEnumerator SlowBulletRoutine()
    {
        yield return new WaitForSeconds(duration);
        entityVFX.StopVfx(fuseEffect);
        entityVFX.PlayVfx(explosionEffect, Vector3.zero, Quaternion.identity);
        cameraFeel.PlayFeedbacks();

        isUsingSkill = false;
        healthCompo.ApplyDamage(crashDamage);

        float prevSpeed = _mover.MoveSpeed;

        _mover.MoveSpeed /= 10;
        DOVirtual.DelayedCall(3f, () => { DOTween.To(() => _mover.MoveSpeed, x => _mover.MoveSpeed = x, prevSpeed, healingCrash); });

        foreach (Enemy obj in EnemyManager.Enemies)
        {
            Debug.Log(obj);
            obj.GetCompo<EntityHealthCompo>().ApplyDamage(100f);
        }
    }
}
