using Member.Ysc._01_Code.Agent;
using System.Collections;
using UnityEngine;

public class SelfBoomSkill : Skill
{
    private EntityVFX entityVFX;
    private readonly string fuseEffect = "BombFuse";

    [Header("터지는 시간")]
    public float duration;
    [Header("흔들림 강도")]
    public float intensity;

    [SerializeField]
    private GameEventChannelSO cameraSO;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        entityVFX = _entity.GetCompo<EntityVFX>();
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
        foreach (Enemy obj in EnemyManager.Enemies)
        {
            obj.GetCompo<EntityHealthCompo>().ApplyDamage(100f,Vector2.left);
        }

        ShakeEvent shakeEvent = CamaraEvents.CameraShakeEvent;
        shakeEvent.intensity = intensity;
        cameraSO.RaiseEvent(shakeEvent);
    }
}
