using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayEffectToTarget", story: "Play [Effect] To [Target] With [StartPos] [CanTargetPos] On [PatternEffect]", category: "Action", id: "b798232ef3906612e303657357206f9f")]
public partial class PlayEffectToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAttack> Effect;
    [SerializeReference] public BlackboardVariable<Entity> _boss;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Transform> StartPos;
    [SerializeReference] public BlackboardVariable<bool> CanTargetPos;
    [SerializeReference] public BlackboardVariable<GameObject> PatternEffect;

    
    protected override Status OnStart()
    {
        if (CanTargetPos.Value)
        {
            PatternEffect.Value = GameObject.Instantiate(Effect.Value.gameObject, Target.Value.position, Quaternion.identity);
            Effect.Value._entity = _boss.Value;
        }
        else
        {
            PatternEffect.Value = GameObject.Instantiate(Effect.Value.gameObject, StartPos.Value.position, Effect.Value.transform.rotation);
            Effect.Value._entity = _boss.Value;

        }
        return Status.Success;
    }
}

