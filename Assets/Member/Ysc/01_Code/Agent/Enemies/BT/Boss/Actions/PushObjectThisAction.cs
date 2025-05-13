using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PushObjectThis", story: "[Self] Object Push", category: "Action", id: "1e354f5eed37797be8005bea87c5ce5f")]
public partial class PushObjectThisAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Self;
    protected override Status OnStart()
    {
        Self.Value.DestroyEnemy();
        return Status.Success;
    }
}

