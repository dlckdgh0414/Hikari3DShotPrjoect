using Member.Ysc._01_Code.Combat.Bullet;
using System.Collections;
using UnityEngine;

public class SlowBulletSkill : Skill
{
    [Header("지속시간")]
    public float slowDuration;
    [Header("느려지는 정도")]
    public float slowDegree;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        BaseBullet.SlowyDegree = slowDegree;
    }

    public override void OverSkillCooltime()
    {
        base.OverSkillCooltime();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        StartCoroutine(SlowBulletRoutine());
    }

    private IEnumerator SlowBulletRoutine()
    {
        BaseBullet.isSlowy = true;
        yield return new WaitForSeconds(slowDuration);
        BaseBullet.isSlowy = false;
    }
}
