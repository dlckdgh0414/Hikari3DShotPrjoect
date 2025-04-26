using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "isDuration", story: "Is [DurationTime] End", category: "Conditions", id: "48aa5b5c20deca3a9019f5c95f558c09")]
public partial class IsDurationCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> DurationTime;

    private float _currentTime;
    
    public override bool IsTrue()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > DurationTime)
        {
            _currentTime = 0;
            return true;
        }
        return false;
    }
}
