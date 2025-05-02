using DG.Tweening;
using UnityEngine;
using Unity.Behavior;

namespace Member.Ysc._01_Code.Agent.Enemies.BT
{
    public abstract class BTEnemy : Enemy
    {
       
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

        }

        [ContextMenu("Dead")]
        protected override void HandleDead()
        {
            if (IsDead) return;
            gameObject.layer = DeadBodyLayer;
            IsDead = true;
            _stateChannel.SendEventMessage(BTEnemyState.DEATH);
        }

        [ContextMenu("Enemy Dead")]
        public void EnemyDead()
        {

            // movement.isMove = false;
            transform.DORotate(new Vector3(-35f, 0f, 0f), 0.5f, RotateMode.Fast)
                .OnUpdate(() =>
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.forward.y, transform.forward.z * -0.4f);
                    transform.DOMove(pos, 10f);
                }).OnComplete(()=>IsDeadEnd = true);
        }

    }
}