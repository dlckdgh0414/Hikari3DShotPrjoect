using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossPatternAttack", story: "Attack To [Target] With [PlayEffect] int [StartPos]", category: "Action", id: "c548008691f055cab4004a8af2973741")]
public partial class BossPatternAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<ParticleSystem> PlayEffect;
    [SerializeReference] public BlackboardVariable<Transform> StartPos;

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

