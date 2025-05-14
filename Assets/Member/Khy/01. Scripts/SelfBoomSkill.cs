using Member.Ysc._01_Code.Agent;
using System.Collections;
using UnityEngine;

public class SelfBoomSkill : Skill
{
    private EntityVFX entityVFX;
    private readonly string fuseEffect = "BombFuse";
    private readonly string explosionEffect = "Explosion";

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
        entityVFX.PlayVfx(explosionEffect, Vector3.zero, Quaternion.identity);
        ShakeEvent shakeEvent = CamaraEvents.CameraShakeEvent;
        shakeEvent.intensity = intensity;
        cameraSO.RaiseEvent(shakeEvent);

        foreach (Enemy obj in EnemyManager.Enemies)
        {
            Debug.Log(obj);
            obj.GetCompo<EntityHealthCompo>().ApplyDamage(100f, Vector2.left);
        }
    }
}
