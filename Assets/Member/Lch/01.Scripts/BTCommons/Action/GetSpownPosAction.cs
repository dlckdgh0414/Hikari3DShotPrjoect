using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetSpownPos", story: "[Dir] to [GetMovePos]", category: "Action", id: "a2301b77c30df666c6c14227b49579c1")]
public partial class GetSpownPosAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Dir;
    [SerializeReference] public BlackboardVariable<EnemyMovement> GetMovePos;
    protected override Status OnStart()
    {
        Dir.Value = GetMovePos.Value.GetMovePos();
        return Status.Success;
    }
}

