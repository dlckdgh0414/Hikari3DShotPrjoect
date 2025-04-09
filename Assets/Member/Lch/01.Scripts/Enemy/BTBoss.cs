using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.Agent.Enemy.BT;
using Unity.Behavior;
using UnityEngine;

public abstract class BTBoss : BTEnemy
{
    private BossStateChangeEvent _stateChannel;
    private BlackboardVariable<BTBossStat> _state;
    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        btAgent = GetComponent<BehaviorGraphAgent>();
        Debug.Assert(btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
        Debug.Log("BT에너미 후 초기화");
    }


    protected override void Start()
    {
        BlackboardVariable<BossStateChangeEvent> stateChannelVariable =
            GetBlackboardVariable<BossStateChangeEvent>("StateChannel");
        _stateChannel = stateChannelVariable.Value;
        Debug.Assert(_stateChannel != null, $"StateChannel variable is null {gameObject.name}");

        _state = GetBlackboardVariable<BTBossStat>("EnemyState");
    }
}
