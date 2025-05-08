using DG.Tweening;
using Member.Ysc._01_Code.Agent;
using System.Collections;
using UnityEngine;

public class HealingSkill : Skill
{
    [Header("지속시간")]
    public float duration;
    [Header("힐량")]
    public float healAmount;

    private EntityVFX entityVFX;
    private EntityHealth healthCompo;
    private readonly string healSkill = "HealVFX";

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        entityVFX = _entity.GetCompo<EntityVFX>();
        healthCompo = _entity.GetCompo<EntityHealth>();
    }

    public override void OverSkillCooltime()
    {
        base.OverSkillCooltime();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        healthCompo.ApplyHeal(healAmount,duration);
        //entityVFX.PlayVfx(healSkill, Vector3.zero, Quaternion.identity);
    }
}
