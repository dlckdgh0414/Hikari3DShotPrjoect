using Member.Ysc._01_Code.Agent.Enemy.BT;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SpawnEnemy", story: "Spawn to [Self] to In [Pos]", category: "Action", id: "d4f376c354b5336e5a415adc0aa5c7a8")]
public partial class SpawnEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<Vector3>> Pos;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

