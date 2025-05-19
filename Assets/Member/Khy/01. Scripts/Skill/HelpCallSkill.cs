using UnityEngine;

public class HelpCallSkill : ActiveSkill
{
    private readonly string CallSkill = "MultiLazerVFX";

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
    }

    public override void OverSkillCooltime()
    {
        base.OverSkillCooltime();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        entityVFX.PlayVfx(CallSkill, Vector3.zero, Quaternion.identity);
    }
}
