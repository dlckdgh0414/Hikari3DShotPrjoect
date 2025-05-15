using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using System.Collections;
using UnityEngine;

public class SelfBoomSkill : Skill
{
    private EntityVFX entityVFX;
    private EntityMover _mover;
    private readonly string fuseEffect = "BombFuse";
    private readonly string explosionEffect = "Explosion";

    [Header("터지는 시간")]
    public float duration;
    [Header("흔들림 강도")]
    public float intensity;
    [Header("폭발 후 회복 속도")]
    public float healingCrash = 1f;

    [SerializeField]
    private GameEventChannelSO cameraSO;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        entityVFX = _entity.GetCompo<EntityVFX>();
        _mover = entity.GetCompo<EntityMover>();
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
        ShakeEvent shakeEvent = CamaraEvents.CameraShakeEvent;
        shakeEvent.intensity = intensity;
        cameraSO.RaiseEvent(shakeEvent);

        float prevSpeed = _mover.MoveSpeed;

        _mover.MoveSpeed /= 10;
        DOVirtual.DelayedCall(3f, () => { DOTween.To(() => _mover.MoveSpeed, x => _mover.MoveSpeed = x, prevSpeed, healingCrash); });

        foreach (Enemy obj in EnemyManager.Enemies)
        {
            Debug.Log(obj);
            obj.GetCompo<EntityHealthCompo>().ApplyDamage(100f, Vector2.left);
        }
    }
}
