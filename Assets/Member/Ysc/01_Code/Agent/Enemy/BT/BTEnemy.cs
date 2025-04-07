using UnityEngine;
using Unity.Behavior;

namespace Member.Ysc._01_Code.Agent.Enemy.BT
{
    public abstract class BTEnemy : Entity
    {
        protected BehaviorGraphAgent btAgent;
        private StateEventChange _stateChannel;
        private BlackboardVariable<BTEnemyState> _state;
        [field: SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }

        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            btAgent = GetComponent<BehaviorGraphAgent>();
            Debug.Assert(btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
            Debug.Log("BT에너미 후 초기화");
        }


        protected virtual void Start()
        {
            BlackboardVariable<StateEventChange> stateChannelVariable =
                GetBlackboardVariable<StateEventChange>("StateChannel");
            _stateChannel = stateChannelVariable.Value;
            Debug.Assert(_stateChannel != null, $"StateChannel variable is null {gameObject.name}");

            _state = GetBlackboardVariable<BTEnemyState>("EnemyState");
        }

        public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
        {
            if (btAgent.GetVariable(key, out BlackboardVariable<T> result))
            {
                return result;
            }

            return default;
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