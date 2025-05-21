using UnityEngine;

public class DeadState : PlayerState
{
    public DeadState(Entity entity) : base(entity)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
    }
}
