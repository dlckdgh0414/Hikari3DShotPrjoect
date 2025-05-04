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

        _state = GetBlackboardVariable<BTBossStat>("EnemyState");
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
        _stateChannel.SendEventMessage(BTEnemyState.DEATH);
    }
    
    public void EnemyDead()
    {

        // movement.isMove = false;
        transform.DORotate(new Vector3(-35f, 0f, 0f), 0.5f, RotateMode.Fast)
            .OnUpdate(() =>
            {
                Vector3 pos = new Vector3(transform.position.x, transform.forward.y, transform.forward.z * -0.4f);
                transform.DOMove(pos, 10f);
            }).OnComplete(() => IsDeadEnd = true);
    }

}
