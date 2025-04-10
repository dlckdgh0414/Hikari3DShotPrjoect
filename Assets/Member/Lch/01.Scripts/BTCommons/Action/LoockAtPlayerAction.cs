using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LoockAtPlayer", story: "Look [Target] In [Self]", category: "Action", id: "64ffc2b83423211ae0a33c370f4bcdbc")]
public partial class LoockAtPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Enemy> Self;
    protected override Status OnStart()
    {
        Self.Value.LookTarget(Target);
        return Status.Success;
    }
}

