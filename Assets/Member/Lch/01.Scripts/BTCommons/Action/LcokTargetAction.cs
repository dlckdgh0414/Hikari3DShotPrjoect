using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LcokTarget", story: "[self] Locck [Target]", category: "Action", id: "7d345873a6f95ced62b5685c0edde4f4")]
public partial class LcokTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Quaternion quaternion = Quaternion.LookRotation(Target.Value.transform.position);
       Self.Value.transform.rotation = quaternion;
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

