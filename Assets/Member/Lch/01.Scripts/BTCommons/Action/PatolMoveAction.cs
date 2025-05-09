using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using static UnityEngine.GraphicsBuffer;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PatolMove", story: "Patol to [Dir] To [Mover] in Camera", category: "Action", id: "849e2886880bdbd048fb6fad5e68fcce")]
public partial class PatolMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Dir;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Mover;
    protected override Status OnStart()
    {
        Mover.Value.PatrolMove(Dir);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Mover.Value.isArrive)
        {
            Mover.Value.isArrive = false;
            return Status.Success;
        }
        return Status.Running;
    }
}

