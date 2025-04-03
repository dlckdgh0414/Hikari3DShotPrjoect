using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayEffect", story: "Play [Effect]", category: "Action", id: "2f9d79d15f16153a1eb8a8f8f50d7751")]
public partial class PlayEffectAction : Action
{
    [SerializeReference] public BlackboardVariable<ParticleSystem> Effect;

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

