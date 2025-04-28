using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Entity entity) : base(entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _mover.SetDecrease(0.1f);
    }

    public override void Update()
    {
        base.Update();
        Vector2 input = _player.InputReader.InputDirection;

        if(input.magnitude > _inputThreshold)
        {
            _player.ChangeState("MOVE");
        }
    }
}
