using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public EntityState CurrentState { get; private set; }
    private Dictionary<string, EntityState> _states;

    public StateMachine(Entity entity, StateListSO stateList)
    {
        _states = new();
        foreach(StateSO state in stateList.states)
        {
            Type t = Type.GetType(state.className);
            Debug.Assert(entity != null, $"Finding type is null : {state.className}"); //안전코드
            EntityState entityState = Activator.CreateInstance(t, new object[] { entity }) as EntityState;

            _states.Add(state.stateName,entityState);
        }
    }

    public void ChangeState(string newStateName)
    {
        CurrentState?.Exit();
        EntityState newState = _states.GetValueOrDefault(newStateName);
        Debug.Assert(newState != null,"존재하지 않는 스테이트가 리스트에 들어갔어요");
        
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void UpdateStateMachine()
    {
        CurrentState.Update();
    }
}
