using DG.Tweening;
using Member.Ysc._01_Code.Combat.Bullet;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class SlowBulletSkill : ActiveSkill
{
    [Header("지속시간")]
    public float slowDuration;
    [Header("느려지는 정도")]
    public float slowDegree;

    public Volume[] volume;
    [SerializeField] private Ami.BroAudio.SoundID ticktackSFX;

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
        Ami.BroAudio.BroAudio.Play(ticktackSFX);
        for(int i=0; i< volume.Length;i++)
        {
            volume[i].gameObject.SetActive(true);
        }
        StartCoroutine(SlowBulletRoutine());
    }

    private IEnumerator SlowBulletRoutine()
    {
        BaseBullet.isSlowy = true;
        yield return new WaitForSeconds(slowDuration);
        BaseBullet.isSlowy = false;
        for (int i = 0; i < volume.Length; i++)
        {
            volume[i].gameObject.SetActive(false);
        }
        Ami.BroAudio.BroAudio.Stop(ticktackSFX);
    }
}
