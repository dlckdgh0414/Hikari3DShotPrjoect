using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SpeedUpSkill : ActiveSkill
{
    [Header("���ƿ��� �ð�")]
    public float duration;

    [Header("���ǵ� Ʈ����")]
    public TrailRenderer _speedTrail;

    [Header("������ ����")]
    public float speedUpIntensity = 1.5f;
    public float atkSpeedUpIntensity = 1.5f;

    private readonly string speedEffect = "SpeedUpVFX";
    private PlayerAttackCompo _attackCompo;

    private void Start()
    {
        _speedTrail.gameObject.SetActive(false);
    }

    public override void UseSkill()
    {
        base.UseSkill();
        _attackCompo = _player.GetCompo<PlayerAttackCompo>();
        entityVFX.PlayVfx(speedEffect, Vector3.zero, Quaternion.identity);
        _speedTrail.gameObject.SetActive(true);
        float prevSpeed = _mover.MoveSpeed;
        float prevAtk = _attackCompo.FireRate;

        _attackCompo.FireRate *= atkSpeedUpIntensity;
        _mover.MoveSpeed *= speedUpIntensity;
        DOVirtual.DelayedCall(3f, () => {
            DOTween.To(() => _mover.MoveSpeed, x => _mover.MoveSpeed = x, prevSpeed,duration);
            DOTween.To(() => _attackCompo.FireRate, x => _attackCompo.FireRate = x, prevAtk,duration);
        })
            .OnComplete(()=> _speedTrail.gameObject.SetActive(false));
    }
}
