using Member.Ysc._01_Code.Agent;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class RevivedPassive : PassiveSkill
{
    private readonly string revinvingVFX = "RevivingVFX";
    private readonly string revinveFinishVFX = "RevivedFinishVFX";
    private EntityHealthCompo _healthCompo;

    [SerializeField]
    private MMF_Player shake;
    [SerializeField]
    private MMF_Player Impulse;

    [Header("부활 대기 시간")]
    [SerializeField]
    private float revivedTime=3f;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        _healthCompo = entity.GetCompo<EntityHealthCompo>();
        _healthCompo.IsRevived = true;
    }

    public void Revived()
    {
        StartCoroutine(RevivedRoutine());
    }

    private IEnumerator RevivedRoutine()
    {
        shake.PlayFeedbacks();

        _mover.CanManualMove = false;
        _entity.IsInvin = true;
        entityVFX.PlayVfx(revinvingVFX,Vector3.zero,Quaternion.identity);
        yield return new WaitForSeconds(revivedTime);
        Impulse.PlayFeedbacks();
        entityVFX.StopVfx(revinvingVFX);
        entityVFX.PlayVfx(revinveFinishVFX, Vector3.zero, Quaternion.identity);
        _healthCompo.ApplyHeal(50f, 2f);
        _mover.CanManualMove = true;
        _entity.IsInvin = false;
        _healthCompo.IsRevived = false;
    }
}
