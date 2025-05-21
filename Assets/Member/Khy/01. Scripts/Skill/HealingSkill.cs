using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using System.Collections;
using UnityEngine;

public class HealingSkill : ActiveSkill
{
    [Header("지속시간")]
    public float duration;
    [Header("힐량")]
    public float healAmount;

    private EntityHealthCompo healthCompo;
    private readonly string healSkill = "HealVFX";

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        healthCompo = _entity.GetCompo<EntityHealthCompo>();
    }

    public override void OverSkillCooltime()
    {
        base.OverSkillCooltime();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        entityVFX.PlayVfx(healSkill, Vector3.zero, Quaternion.identity);
        healthCompo.ApplyHeal(healAmount,duration);
        //entityVFX.PlayVfx(healSkill, Vector3.zero, Quaternion.identity);
    }
}
