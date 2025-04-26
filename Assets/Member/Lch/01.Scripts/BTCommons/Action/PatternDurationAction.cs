using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PatternDuration", story: "[PatternEffect] is [Duration]", category: "Action", id: "6d6ce1a86336be0fa2b06dc955ae962a")]
public partial class PatternDurationAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAttack> PatternEffect;
    [SerializeReference] public BlackboardVariable<float> Duration;
    
    
    private float _currentTime;
    protected override Status OnStart()
    {
        _currentTime = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > Duration.Value)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

