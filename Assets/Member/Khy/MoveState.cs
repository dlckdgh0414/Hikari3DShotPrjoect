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
        ClampPosition();

        Vector2 moveInput = _player.InputReader.InputDirection;

        if (_mover.CanManualMove)
            _mover.SetMovement(moveInput);

        if (moveInput.magnitude < _inputThreshold || !_mover.CanManualMove)
            _player.ChangeState("IDLE");
    }

    

    private void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(_player.transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        _player.transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
