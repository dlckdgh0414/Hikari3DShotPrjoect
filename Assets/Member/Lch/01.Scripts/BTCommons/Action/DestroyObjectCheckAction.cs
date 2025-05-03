using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DestroyObjectCheck", story: "Destory Check to [Enemy]", category: "Action", id: "2620d06596faa4f2fcbe6fba74e45764")]
public partial class DestroyObjectCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Enemy;

    protected override Status OnStart()
    {
        Enemy.Value.EnemyDead();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(Enemy.Value.IsDeadEnd)
        {
            return Status.Success;
        }

        return Status.Running;
    }
}

