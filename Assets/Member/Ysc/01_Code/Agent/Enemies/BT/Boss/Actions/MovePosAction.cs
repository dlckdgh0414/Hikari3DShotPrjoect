using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MovePos", story: "[Target] MovePos To [Pos]", category: "Action", id: "037a0f4d9a2145757f406d874e206307")]
public partial class MovePosAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Vector3> Pos;
    protected override Status OnStart()
    {
        Target.Value.position += new Vector3(Pos.Value.x, Pos.Value.y, Pos.Value.z);
        return Status.Success;
    }
}

