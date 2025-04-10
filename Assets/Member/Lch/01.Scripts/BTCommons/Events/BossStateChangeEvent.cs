using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/BossStateChangeEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "BossStateChangeEvent", message: "boss state change to [State]", category: "Events", id: "d91360a16bcddd93fc75f3bb056f14ee")]
public partial class BossStateChangeEvent : EventChannelBase
{
    public delegate void BossStateChangeEventEventHandler(BTBossStat State);
    public event BossStateChangeEventEventHandler Event; 

    public void SendEventMessage(BTBossStat State)
    {
        Event?.Invoke(State);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<BTBossStat> StateBlackboardVariable = messageData[0] as BlackboardVariable<BTBossStat>;
        var State = StateBlackboardVariable != null ? StateBlackboardVariable.Value : default(BTBossStat);

        Event?.Invoke(State);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        BossStateChangeEventEventHandler del = (State) =>
        {
            BlackboardVariable<BTBossStat> var0 = vars[0] as BlackboardVariable<BTBossStat>;
            if(var0 != null)
                var0.Value = State;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as BossStateChangeEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as BossStateChangeEventEventHandler;
    }
}

