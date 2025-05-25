using System.Collections;
using UnityEngine;

public class ShieldSkill : ActiveSkill
{
    private readonly string shieldSkill = "ShieldVFX";
    private readonly string shieldSkillDelete = "ShieldDelete";
    [Header("무적시간")]
    [SerializeField]
    private float invinTime =1f;

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
        _entity.IsInvin = true;
        StartCoroutine(InvinRoutine());
        entityVFX.PlayVfx(shieldSkill,Vector3.zero,Quaternion.identity);
    }

    private IEnumerator InvinRoutine()
    {
        yield return new WaitForSeconds(invinTime);
        entityVFX.StopVfx(shieldSkill);
        _entity.IsInvin = false;

        entityVFX.PlayVfx(shieldSkillDelete,Vector3.zero,Quaternion.identity);
    }
}
