using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayEffectToTarget", story: "Play [Effect] To [Target] With [StartPos] [CanTargetPos]", category: "Action", id: "b798232ef3906612e303657357206f9f")]
public partial class PlayEffectToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Effect;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Transform> StartPos;
    [SerializeReference] public BlackboardVariable<bool> CanTargetPos;

    protected override Status OnStart()
    {
        if (CanTargetPos.Value)
        {
            GameObject.Instantiate(Effect.Value, Target.Value.position, Quaternion.identity);
        }
        else
        {
            GameObject.Instantiate(Effect.Value,StartPos.Value.position, Effect.Value.transform.rotation);
        }
        return Status.Success;
    }
}

