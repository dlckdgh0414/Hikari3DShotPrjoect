using DG.Tweening;
using UnityEngine;
using Unity.Behavior;

namespace Member.Ysc._01_Code.Agent.Enemies.BT
{
    public abstract class BTEnemy : Enemy
    {
        private StateEventChange _stateChannel;
        private BlackboardVariable<BTEnemyState> _state;

        protected override void Start()
        {
            BlackboardVariable<StateEventChange> stateChannelVariable =
                GetBlackboardVariable<StateEventChange>("StateChannel");
            _stateChannel = stateChannelVariable.Value;
            Debug.Assert(_stateChannel != null, $"StateChannel variable is null {gameObject.name}");

            _state = GetBlackboardVariable<BTEnemyState>("EnemyState");
        }

        protected override void HandleHit()
        {
            if (IsDead) return;

            _stateChannel.SendEventMessage(BTEnemyState.HIT);
        }

        protected override void HandleDead()
        {
            if (IsDead) return;
            gameObject.layer = DeadBodyLayer;
            IsDead = true;
            _stateChannel.SendEventMessage(BTEnemyState.DEATH);
        }
        
        
    }
}