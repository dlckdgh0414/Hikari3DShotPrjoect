using UnityEngine;

public abstract class EntityState
{
    protected Entity _entity;

    public EntityState(Entity entity)
    {
        _entity = entity;
    }

    public virtual void Enter()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
