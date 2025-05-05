using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.UIElements;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RotateToLookTarget", story: "[Target] rotate to [LookTarget] and [Duration] is Smoothly [IsSmooth]", category: "Action", id: "f651f9893517868416ebae4111e3514f")]
public partial class RotateToLookTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Transform> LookTarget;
    [SerializeReference] public BlackboardVariable<float> Duration;
    [SerializeReference] public BlackboardVariable<bool> IsSmooth;

    protected override Status OnStart()
    {
        TargetToRotate();
        return Status.Success;
    }

    private void TargetToRotate()
    {
        Quaternion q = Quaternion.LookRotation(LookTarget.Value.position - Target.Value.position);
        if (IsSmooth)
        {
            Target.Value.rotation =  Quaternion.Lerp(Target.Value.rotation, q, Duration.Value);
        }
        else
        {
            Target.Value.rotation = q;
        }
    }
}

