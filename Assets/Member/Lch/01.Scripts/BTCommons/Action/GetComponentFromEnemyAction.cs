using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;
using Member.Ysc._01_Code.Agent.Enemies.BT;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetComponentFromEnemy", story: "Get components from [btEnemy]", category: "Action", id: "fc71b4eb657e03f9c590f6d37a925290")]
public partial class GetComponentFromEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> BtEnemy;

    protected override Status OnStart()
    {
        Debug.Assert(BtEnemy != null,$"¾Ó ±â¸ð¶ì");
        Enemy enemy = BtEnemy.Value;
        SetVariableToBT(enemy, "Mover", enemy.GetCompo<EnemyMovement>());
        SetVariableToBT(enemy, "EnemyAttack", enemy.GetComponentInChildren<Attack>());

        return Status.Success;
    }

    private void SetVariableToBT<T>(Enemy enemy, string variableName, T component)
    {
        Debug.Assert(component != null, $"Check {variableName} component exist on {enemy.gameObject.name}");
        BlackboardVariable<T> variable = enemy.GetBlackboardVariable<T>(variableName);
        variable.Value = component;
    }
}

