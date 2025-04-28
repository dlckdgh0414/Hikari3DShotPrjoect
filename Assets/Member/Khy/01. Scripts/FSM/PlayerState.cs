using DG.Tweening;
using System;
using UnityEngine;


public class PlayerState : EntityState
{
    protected Player _player;
    protected EntityMover _mover;
    protected EntityVFX entityVFX;
    protected readonly float _inputThreshold = 0.1f;
    private readonly string dodgeSkill = "DodgeVFX";

    public PlayerState(Entity entity) : base(entity)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
        entityVFX = entity.GetCompo<EntityVFX>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputReader.OnWingEvent += OnWingHandle;
        _player.InputReader.OnFirSkillEvent += OnFirSkillHandle;
        _player.InputReader.OnSecSkillEvent += OnSecSkillHandle;
        _player.InputReader.OnThrSkillEvent += OnThrSkillHandle;
    }

    private void OnFirSkillHandle()
    {
        _player.GetCompo<SkillCompo>().firstSkill.AttemptUseSkill();
    }

    private void OnSecSkillHandle()
    {
        _player.GetCompo<SkillCompo>().secondSkill.AttemptUseSkill();
    }

    private void OnThrSkillHandle()
    {
        _player.GetCompo<SkillCompo>().thirdSkill.AttemptUseSkill();
    }


    private void OnWingHandle(int dir)
    {
        if (_player.GetCompo<SkillCompo>().GetSkill<DodgeSkill>().AttemptUseSkill())
        {
            if (!DOTween.IsTweening(_player.model.transform))
            {
                entityVFX.PlayVfx(dodgeSkill, Vector3.zero, Quaternion.identity);
                _mover.StopImmediately();
                _mover.CanManualMove = false;
                _mover.SetAutoMovement(new Vector3(dir * 25, 0, 0));


                _player.model.transform.DOLocalRotate(new Vector3(_player.transform.localEulerAngles.x, _player.transform.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.OutSine).OnComplete(() => _mover.CanManualMove = true);
            }
        }
    }

    public override void Update()
    {
        base.Update();
        HorizontalLean();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        ClampPosition();
    }

    private void ClampPosition()
    {
        float clampedX = Mathf.Clamp(_player.transform.position.x, -9f, 9f);
        float clampedY = Mathf.Clamp(_player.transform.position.y, -5f, 5f);

        _player.transform.position = new Vector3(clampedX, clampedY, _player.zPos);
    }

    void HorizontalLean()
    {
        Vector3 targetEulerAngels = _player.model.transform.localEulerAngles;
        Vector3 playerRotateDir = _player.InputReader.GetWorldPosition(out RaycastHit hitInfo) - _player.transform.position;
        Quaternion q = Quaternion.LookRotation(playerRotateDir);
        _player.model.transform.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -_player.InputReader.InputDirection.x * 60, 0.1f));
        _player.model.transform.rotation = Quaternion.Lerp(_player.model.transform.rotation,q,0.1f);
    }
}
