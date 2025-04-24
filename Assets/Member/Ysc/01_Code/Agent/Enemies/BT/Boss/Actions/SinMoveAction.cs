using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SinMove", story: "[Self] [Movement] to Sin isX [CanX] isY [CanY] duration [Second]", category: "Action", id: "9cee4493a34d4c9e555ccd9eb975bcf5")]
public partial class SinMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<BTBoss> Self;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Move;
    [SerializeReference] public BlackboardVariable<bool> CanX;
    [SerializeReference] public BlackboardVariable<bool> CanY;
    [SerializeReference] public BlackboardVariable<float> Second;

    private float time = 0;
    
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        time += Time.deltaTime;
        MoveToSine();
        if (Time.time - time < Second.Value)
            return Status.Success;
        return Status.Running;
    }

    private void MoveToSine()
    {
    }

    protected override void OnEnd()
    {
    }
}

