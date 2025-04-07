using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopMove", story: "Stop to [Mover]", category: "Action", id: "09514fbfab6de1db9ab41f9c5de3df4e")]
public partial class StopMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyMovement> Mover;

    protected override Status OnStart()
    {
        Mover.Value.StopMover();
        return Status.Success;
    }
}

