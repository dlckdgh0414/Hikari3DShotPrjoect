using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TargetSetRotate", story: "[Target] set [Rotate] with [Pattern1Effect]", category: "Action",
    id: "5a0e4e7748ff9141e466d21922f8c2d9")]
public partial class TargetSetRotateAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Pattern1Effect;
    [SerializeReference] public BlackboardVariable<Vector3> Rotate;

    protected override Status OnStart()
    {
        Target.Value.rotation = Quaternion.Euler(Vector3.zero);
        Pattern1Effect.Value.transform.rotation = Quaternion.Euler(Vector3.zero);
        return Status.Success;
    }
}

