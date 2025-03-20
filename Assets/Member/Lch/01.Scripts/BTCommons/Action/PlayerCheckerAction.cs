using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Member.Ysc._01_Code.Agent.Enemy.BT;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayerChecker", story: "[self] set [target] from finder", category: "Action", id: "7f5cc3478c9bd1a270fb36305cb14aaa")]
public partial class PlayerCheckerAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    protected override Status OnStart()
    {
        Debug.Assert(Self != null, $"you not Self have");
        if (Self.Value.PlayerFinder.target == null)
        {
            Debug.Log("타겟없음");
            return Status.Failure;
        }
        if (Self.Value.PlayerFinder == null)
        {
            Debug.Log("PlayerFinder없음");
            return Status.Failure;
        }
        Target.Value = Self.Value.PlayerFinder.target.transform;
        return Status.Success;
    }
}

