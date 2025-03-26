using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PotalMoveDir", story: "Get Patol to [MoveDir] To [Mover]", category: "Action", id: "2b733f55bd4780da2658c67d0ead8f97")]
public partial class PotalMoveDirAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> MoveDir;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Mover;

    protected override Status OnStart()
    {
        MoveDir.Value = Mover.Value.GetPatrolMove();
        return Status.Running;
    }
}

