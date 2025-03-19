using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Agent.Enemy.BT;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayerFinde", story: "[self] set [target] from finder", category: "Action", id: "9cbef35a839804726be65f2662c21cab")]
public partial class PlayerFindeAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    protected override Status OnStart()
    {
        Target.Value = Self.Value.PlayerFinder.target.transform;
        Debug.Assert(Target.Value != null, $"Target is null : {Self.Value.gameObject.name}");

        return Status.Success;
    }
}

