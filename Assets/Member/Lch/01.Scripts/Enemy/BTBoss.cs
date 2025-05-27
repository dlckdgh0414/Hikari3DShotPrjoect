using DG.Tweening;
using Unity.Behavior;
using UnityEngine;

public abstract class BTBoss : Enemy
{
    private BossStateChangeEvent _bossstateChannel;
    private BlackboardVariable<BTBossStat> _state;
    protected override void Start()
    {
        BlackboardVariable<BossStateChangeEvent> stateChannelVariable =
            GetBlackboardVariable<BossStateChangeEvent>("StateChannel");
        _bossstateChannel = stateChannelVariable.Value;
        Debug.Assert(_bossstateChannel != null, $"StateChannel variable is null {gameObject.name}");

        _state = GetBlackboardVariable<BTBossStat>("BossState");
    }

    protected override void HandleHit()
    {
        if (IsDead) return;
    }

    protected override void HandleDead()
    {
        if (IsDead) return;
        gameObject.layer = DeadBodyLayer;
        IsDead = true;
        _bossstateChannel.SendEventMessage(BTBossStat.DEATH);
    }
}
