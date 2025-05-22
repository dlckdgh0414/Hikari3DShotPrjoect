using Member.Ysc._01_Code.Combat.Attacker;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckType", story: "Check [AttackType] is LaserAttack", category: "Conditions", id: "d5aaf7ca873c7ed39ae613a494f69665")]
public partial class CheckTypeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> AttackType;

    public override bool IsTrue()
    {
        return AttackType.Value.GetComponent<LaserAttack>();
    }
    
}
