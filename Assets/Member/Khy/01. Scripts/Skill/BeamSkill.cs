using UnityEngine;

public class BeamSkill : ActiveSkill
{
    private readonly string beamSkill = "CoreBeamVFX";
    public override void UseSkill()
    {
        base.UseSkill();
        _entity.IsInvin = true;
        entityVFX.PlayVfx(beamSkill, Vector3.zero, Quaternion.identity);
        isUsingSkill = false;
    }
}
