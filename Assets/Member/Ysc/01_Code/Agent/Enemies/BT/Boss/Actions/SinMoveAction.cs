using System;
using System.Numerics;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Vector3 = UnityEngine.Vector3;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SinMove", story: "[Self] [Movement] to Sin isX [CanX] isY [CanY]", category: "Action", id: "9cee4493a34d4c9e555ccd9eb975bcf5")]
public partial class SinMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<BTBoss> Self;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Move;
    [SerializeReference] public BlackboardVariable<bool> CanX;
    [SerializeReference] public BlackboardVariable<bool> CanY;

    private float time = 0;
    
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        MoveToSine();
        return Status.Success;
    }

    private void MoveToSine()
    {
        time += Time.deltaTime;
        float sin1 = 50f * Mathf.Sin(time);
        float sin2 = 50f * Mathf.Sin(time*2);
        Debug.Log(Mathf.Sin(time));
        Vector3 movePos = new Vector3( sin1 - Self.Value.transform.position.x ,sin2 - Self.Value.transform.position.y)
        {
            z = 0
        };
        Vector3 p = Vector3.Slerp(Self.Value.transform.position, movePos, 0.1f);
        Debug.Log(p);
        Self.Value.transform.position = p;
    }
}

