using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StateEventChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StateEventChange", message: "enemy state change to [state]", category: "Events", id: "59a60c846bffbadb35fc7fe9053f4dd8")]
public partial class StateEventChange : EventChannelBase
{
    public delegate void StateEventChangeEventHandler(BTEnemyState state);
    public event StateEventChangeEventHandler Event; 

    public void SendEventMessage(BTEnemyState state)
    {
        Event?.Invoke(state);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<BTEnemyState> stateBlackboardVariable = messageData[0] as BlackboardVariable<BTEnemyState>;
        var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(BTEnemyState);

        Event?.Invoke(state);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        StateEventChangeEventHandler del = (state) =>
        {
            BlackboardVariable<BTEnemyState> var0 = vars[0] as BlackboardVariable<BTEnemyState>;
            if(var0 != null)
                var0.Value = state;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as StateEventChangeEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as StateEventChangeEventHandler;
    }
}

