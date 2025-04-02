using DG.Tweening;
using System;
using UnityEngine;


public class PlayerState : EntityState
{
    protected Player _player;
    protected EntityMover _mover;
    protected readonly float _inputThreshold = 0.1f;

    public PlayerState(Entity entity) : base(entity)
    {
        _player = entity as Player;
        _mover = entity.GetCompo<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputReader.OnWingEvent += OnWingHandle;
    }

    private void OnWingHandle(int dir)
    {
        if (!DOTween.IsTweening(_player.model.transform))
        {
            _mover.StopImmediately();
            _mover.CanManualMove = false;
            _mover.SetAutoMovement(new Vector3(dir * 25,0,0));
            _player.model.transform.DOLocalRotate(new Vector3(_player.transform.localEulerAngles.x, _player.transform.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd)
                .SetEase(Ease.OutSine).OnComplete(() => _mover.CanManualMove = true);
        }
    }

    public override void Update()
    {
        base.Update();
        HorizontalLean();
    }

    void HorizontalLean()
    {
        Vector3 targetEulerAngels = _player.model.transform.localEulerAngles;
        _player.model.transform.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, _player.InputReader.InputDirection.x * 30, 0.1f));
    }
}
