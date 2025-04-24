        using System;
        using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using VHierarchy.Libs;
using Random = UnityEngine.Random;

public class Roll : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO rollEventChannel;
    [SerializeField] private SkillSOList skillListSO;
    [SerializeField] private RectTransform cotnentPanel;
    [SerializeField] private float scrollSpeed;
    
    //[SerializeField] private float luck;

    public List<RollItem> rollItems = new List<RollItem>();
    private Dictionary<string, RollDataSO> _skillDic/*k*/ = new Dictionary<string, RollDataSO>();
    private RollDataSO _rolledSkill = null;
    
    private readonly RollStartEvent _rollStartEvent = new RollStartEvent();
    private readonly RollEndEvent _rollEndEvent = new RollEndEvent();
    
    private bool _isRolling = false;
    
    private void Awake()
    {
        skillListSO.skillList.ForEach(s =>  _skillDic.Add(s.name, s));
        rollItems.ForEach(item => item.SettingItem(SelectedSkill()));
    }

    private void Update()
    {
        if (_isRolling)
            Rolling();
    }

    private bool IsPicked(float rarity)
    {
        return Random.Range(1, (int)rarity) == 1;
    }

    [ContextMenu("Roll")]
    public void SkillRoll() => _isRolling = true;

    private void Rolling()
    {
        cotnentPanel.anchoredPosition += Vector2.left * (scrollSpeed * Time.deltaTime);
        scrollSpeed /= (1.005f );

        if (scrollSpeed <= 25) RollEnd();

        if (cotnentPanel.anchoredPosition.x <= -215)
        {
            RollItem item = rollItems[0];
            rollItems[0].transform.SetAsLastSibling();
            rollItems[0].SettingItem(SelectedSkill());
            
            rollItems.RemoveAt(0);
            rollItems.Add(item);
            cotnentPanel.anchoredPosition = Vector2.zero;
        }
    }

    private void RollEnd()
    {
        scrollSpeed = 0;

        string rolledSkill = rollItems.OrderBy(x => (((RectTransform)x.transform).position - Vector3.zero).magnitude).Last().name;
        
        Debug.Log(name);

        RollDataSO rolledData = _skillDic
            .Where(x => x.Key == rolledSkill)
            .Select(x => x.Value)
            .FirstOrDefault();
        
        _isRolling = false;
        _rollEndEvent.rolledSkill = rolledData;
        rollEventChannel.RaiseEvent(_rollEndEvent);
    }

    private RollDataSO SelectedSkill()
    {
        RollStartEvent rollStartEvent = RollEventChannel.rollStartEvent;

        foreach (RollDataSO skill in _skillDic.Values.Reverse())
        {
            if (IsPicked(skill.rarity / 1))
            {
                return skill;
            }
        }

        return null;
    }
}
