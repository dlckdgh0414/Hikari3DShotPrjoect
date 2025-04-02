using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EnemyAttack", story: "[Attack] To [Target]", category: "Action", id: "7e81fe03cadae99650582ec67fca5d2d")]
public partial class EnemyAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Attack> Attack;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Attack.Value.EnemyAttack(Target.Value);
        return Status.Success;
    }
}

