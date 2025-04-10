using Unity.Behavior;
using UnityEngine;

public abstract class BTBoss : Enemy
{
    private BossStateChangeEvent _stateChannel;
    private BlackboardVariable<BTBossStat> _state;
    
    protected override void Start()
    {
        BlackboardVariable<BossStateChangeEvent> stateChannelVariable =
            GetBlackboardVariable<BossStateChangeEvent>("StateChannel");
        _stateChannel = stateChannelVariable.Value;
        Debug.Assert(_stateChannel != null, $"StateChannel variable is null {gameObject.name}");

        _state = GetBlackboardVariable<BTBossStat>("EnemyState");
    }

    protected override void HandleHit()
    {
        if (IsDead) return;

        _stateChannel.SendEventMessage(BTBossStat.HIT);
    }

    protected override void HandleDead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        _stateChannel.SendEventMessage(BTBossStat.DEATH);
    }
}
