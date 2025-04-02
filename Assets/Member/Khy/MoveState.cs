using UnityEngine;

public class MoveState : PlayerState
{

    public MoveState(Entity entity) : base(entity)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        Vector2 moveInput = _player.InputReader.InputDirection;

        if (_mover.CanManualMove)
            _mover.SetMovement(moveInput);

        if (moveInput.magnitude < _inputThreshold || !_mover.CanManualMove)
            _player.ChangeState("IDLE");
    }
}
