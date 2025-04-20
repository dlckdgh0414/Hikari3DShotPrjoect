using System;
using System.Collections.Generic;
using UnityEngine;

public class AfterRoll : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO eventChannel;

    private void Awake()
    {
        eventChannel.AddListener<RollEndEvent>(HandleRollEnd);
    }

    private void HandleRollEnd(RollEndEvent rollEvent)
    {
        //if(rollEvent.rolledSkill)
    }
}
