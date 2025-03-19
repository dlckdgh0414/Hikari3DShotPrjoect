using UnityEngine;
using Unity.Behavior;

namespace Member.Ysc._01_Code.Agent.Enemy.BT
{
    public class BTEnemy : Enemy
    {
        protected BehaviorGraphAgent btAgent;

        [field: SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }
        
        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            btAgent = GetComponent<BehaviorGraphAgent>();
            Debug.Assert(btAgent != null, $"{gameObject.name} does not have an BehaviorGraphAgent");
            Debug.Log("BT에너미 후 초기화");
        }

        
        public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
        {
            if (btAgent.GetVariable(key, out BlackboardVariable<T> result))
            {
                return result;
            }

            return default;
        }
    }
}