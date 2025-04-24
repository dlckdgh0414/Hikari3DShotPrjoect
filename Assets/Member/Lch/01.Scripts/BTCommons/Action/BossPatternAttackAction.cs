using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossPatternAttack", story: "Attack To [Target] With [PlayEffect] int [StartPos] [StartTargetPos] on [EffectObj] set [IsSetParent]", category: "Action", id: "c548008691f055cab4004a8af2973741")]
public partial class BossPatternAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EnemyAttack> PlayEffect;
    [SerializeReference] public BlackboardVariable<Transform> StartPos;
    [SerializeReference] public BlackboardVariable<bool> StartTargetPos;
    [SerializeReference] public BlackboardVariable<GameObject> EffectObj;
    [SerializeReference] public BlackboardVariable<Entity> boss;
    [SerializeReference] public BlackboardVariable<bool> IsSetParent;
    protected override Status OnStart()
    {
        if (StartTargetPos.Value)
        {
            EffectObj.Value = GameObject.Instantiate(PlayEffect.Value.gameObject,StartPos.Value.position,Quaternion.identity);
            PlayEffect.Value._entity = boss.Value;
            
        }
        else
        {
            EffectObj.Value = GameObject.Instantiate(PlayEffect.Value.gameObject,StartPos.Value.position, Quaternion.Euler(0, 180f, 0));
            PlayEffect.Value._entity = boss.Value;
        }
        if(IsSetParent.Value)
        {
            EffectObj.Value.transform.SetParent(StartPos.Value);
        }
        return Status.Success;
    }
}

