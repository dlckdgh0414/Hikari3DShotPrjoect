using System;
using Member.Ysc._01_Code.Combat.Attacker;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LaserInit", story: "[AttackTrm] Init", category: "Action", id: "b945fa71c46b8c35829e0831de1a1cb6")]
public partial class LaserInitAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> AttackTrm;

    protected override Status OnStart()
    {
        LaserAttack laser = AttackTrm.Value.GetComponentInChildren<LaserAttack>();
        laser.InitLaser();
        
        return Status.Success;
    }
}

