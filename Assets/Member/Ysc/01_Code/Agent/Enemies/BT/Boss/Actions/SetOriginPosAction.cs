using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetOriginPos", story: "[TargetPos] from [Origin]", category: "Action", id: "39edc5b93fd1737a586ad7bd3f34022f")]
public partial class SetOriginPosAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> TargetPos;
    [SerializeReference] public BlackboardVariable<Vector3> Origin;

    protected override Status OnStart()
    {
        TargetPos.Value.position = Origin.Value;
        return Status.Success;
    }
}

