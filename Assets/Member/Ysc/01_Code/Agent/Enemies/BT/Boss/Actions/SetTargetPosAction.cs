using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTargetPos", story: "[Pos] from [TargetPos]", category: "Action", id: "39edc5b93fd1737a586ad7bd3f34022f")]
public partial class SetTargetPosAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Pos;
    [SerializeReference] public BlackboardVariable<Vector3> TargetPos;
    protected override Status OnStart()
    {
        TargetPos.Value = Pos.Value.localPosition;
        return Status.Success;
    }
}

