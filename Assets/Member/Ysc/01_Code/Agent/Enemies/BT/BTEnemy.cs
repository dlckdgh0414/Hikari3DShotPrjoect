using DG.Tweening;
using UnityEngine;
using Unity.Behavior;

namespace Member.Ysc._01_Code.Agent.Enemies.BT
{
    public abstract class BTEnemy : Enemy
    {
        [SerializeField] private int minCurrency = 5;
        [SerializeField] private int maxCurrency = 50;
        
        private BlackboardVariable<BTEnemyState> _state;
        
        protected override void Start()
        {
            BlackboardVariable<StateEventChange> stateChannelVariable =
                GetBlackboardVariable<StateEventChange>("StateChannel");
            _stateChannel = stateChannelVariable.Value;
            Debug.Assert(_stateChannel != null, $"StateChannel variable is null {gameObject.name}");

            _state = GetBlackboardVariable<BTEnemyState>("EnemyState");
        }

        public override void InitObject()
        {
            if (_stateChannel == null) return;
            _stateChannel.SendEventMessage(BTEnemyState.SPAWN);
        }

        protected override void HandleHit()
        {
            Debug.Log("쳐맞음");
            if (IsDead) return;
        }

        [ContextMenu("Dead")]
        public void Dead()
        {
            if (IsDead) return;
            OnDead?.Invoke();
        }

        protected override void HandleDead()
        {
            if (IsDead) return;
            IsDead = true;
            gameObject.layer = DeadBodyLayer;
            GetComponentInChildren<EntityVFX>().PlayVfx("DeathVFX", Vector3.zero, Quaternion.identity);
            CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, Random.Range(minCurrency, maxCurrency+1));
            _stateChannel.SendEventMessage(BTEnemyState.DEATH);
        }
    }
}