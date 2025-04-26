using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SpawnEnemy", story: "Spawn to [Target] to In [Mover] in [Dir]", category: "Action", id: "d4f376c354b5336e5a415adc0aa5c7a8")]
public partial class SpawnEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EnemyMovement> Mover;
    [SerializeReference] public BlackboardVariable<Vector3> Dir;
    protected override Status OnStart()
    {
        Debug.Assert(Mover.Value != null ,"Dffafd");
        Debug.Assert(Dir.Value != null, "dfafsff");
        Dir.Value = Mover.Value.GetMovePos();
        return Status.Running;
    }
    protected override Status OnUpdate()
    {
        Mover.Value.Move(Target.Value, Dir);
        if (Mover.Value.isArrive)
        {
            Mover.Value.isArrive = false;
            Mover.Value.isMove = true;
            return Status.Success;
        }
        return Status.Running;
    }
}

