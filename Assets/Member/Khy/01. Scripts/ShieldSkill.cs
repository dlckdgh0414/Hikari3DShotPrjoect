using UnityEngine;

public class ShieldSkill : Skill
{
    private readonly string shieldSkill = "ShieldVFX";

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
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
