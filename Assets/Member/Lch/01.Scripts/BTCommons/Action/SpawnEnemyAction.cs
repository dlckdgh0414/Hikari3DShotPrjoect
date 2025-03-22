using Member.Ysc._01_Code.Agent.Enemy.BT;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SpawnEnemy", story: "Spawn to [Target] to In [Mover]", category: "Action", id: "d4f376c354b5336e5a415adc0aa5c7a8")]
public partial class SpawnEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Mover;
    protected override Status OnStart()
    {
        Mover.Value.Move(Target.Value,Mover.Value.GetMovePos());
        return Status.Success;
    }
}

