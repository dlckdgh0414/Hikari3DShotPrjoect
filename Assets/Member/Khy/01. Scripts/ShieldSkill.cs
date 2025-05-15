using UnityEngine;

public class ShieldSkill : Skill
{
    private EntityVFX entityVFX;
    private readonly string shieldSkill = "ShieldVFX";

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        entityVFX = _entity.GetCompo<EntityVFX>();
    }

    public override void OverSkillCooltime()
    {
        base.OverSkillCooltime();
        _entity.IsInvin = false;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        _entity.IsInvin = true;
        entityVFX.PlayVfx(shieldSkill,Vector3.zero,Quaternion.identity);
    }
}
