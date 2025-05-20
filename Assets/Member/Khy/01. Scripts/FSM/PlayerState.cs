using DG.Tweening;
using System;
using UnityEngine;


public class PlayerState : EntityState
{
    protected Player _player;
    protected EntityMover _mover;
    protected readonly float _inputThreshold = 0.1f;
    private AutoAimCompo _aimCompo;


    public PlayerState(Entity entity) : base(entity)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
        _aimCompo = entity.GetCompo<AutoAimCompo>();
    }

    public override void Enter()
    {
        base.Enter();
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
        Vector3 playerRotateDir;

        if (_aimCompo.IsAutoAim)
            playerRotateDir = _aimCompo.target.transform.position - _player.transform.position;
        else
            playerRotateDir = _player.InputReader.GetWorldPosition(out RaycastHit hitInfo) - _player.transform.position;

        _player.model.transform.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngels.x,-playerRotateDir.y + -_player.InputReader.InputDirection.y * 40, 0.1f), Mathf.LerpAngle(targetEulerAngels.y, playerRotateDir.x, 0.1f), Mathf.LerpAngle(targetEulerAngels.z, -_player.InputReader.InputDirection.x * 26, 0.1f));
        //_player.model.transform.rotation = Quaternion.Lerp(_player.model.transform.rotation,q,0.5f);
    }
}
