using UnityEngine;

public class DeadState : PlayerState
{
    private EntityVFX entityVFX;
    public DeadState(Entity entity) : base(entity)
    {
        entityVFX = entity.GetCompo<EntityVFX>();
    }
    public override void Enter()
    {
        base.Enter();
        _mover.CanManualMove = false;
        _mover.SetAutoMovement(Vector3.down);
        entityVFX.PlayVfx("DeathVFX", Vector3.zero,Quaternion.identity);
    }

    public override void Update()
    {
        base.Update();
        
    }
}
