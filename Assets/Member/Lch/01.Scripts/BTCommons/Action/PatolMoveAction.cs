using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PatolMove", story: "Patol to [minXY] and [maxXY] To [Mover] in Camera", category: "Action", id: "849e2886880bdbd048fb6fad5e68fcce")]
public partial class PatolMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector2> MinXY;
    [SerializeReference] public BlackboardVariable<Vector2> MaxXY;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Mover;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Mover.Value.PatolMove(MinXY, MaxXY);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

