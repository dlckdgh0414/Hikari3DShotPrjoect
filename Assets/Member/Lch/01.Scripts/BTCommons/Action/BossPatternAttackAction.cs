using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossPatternAttack", story: "Attack To [Target] With [PlayEffect] int [StartPos] [StartTargetPos]", category: "Action", id: "c548008691f055cab4004a8af2973741")]
public partial class BossPatternAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<GameObject> PlayEffect;
    [SerializeReference] public BlackboardVariable<Transform> StartPos;
    [SerializeReference] public BlackboardVariable<bool> StartTargetPos;
    protected override Status OnStart()
    {
        if (StartTargetPos.Value)
        {
            GameObject.Instantiate(PlayEffect.Value,Target.Value.position,Quaternion.identity);
        }
        else
        {
            GameObject.Instantiate(PlayEffect.Value,StartPos.Value.position, Quaternion.Euler(0, 180f, 0));
        }
        return Status.Success;
    }
}

