using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillInventory : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO eventChannel;
    public Dictionary<RollDataSO, int> _ownSkillDic { get; private set; }
        = new Dictionary<RollDataSO, int>();

    private void Awake()
    {
        eventChannel.AddListener<RollEndEvent>(HandleRollEnd);
    }

    private void HandleRollEnd(RollEndEvent obj)
    {
        RollDataSO rolledSkill = obj.rolledSkill;

        if (!_ownSkillDic.ContainsKey(rolledSkill))
        {
            _ownSkillDic.Add(rolledSkill, 1);
        }
        else
        {
            _ownSkillDic[rolledSkill]++;
            CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, 100);
        }

        foreach (var skill in _ownSkillDic)
        {
            Debug.Log(skill);
        }
    }
}
