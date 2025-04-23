        using System;
        using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Random = UnityEngine.Random;

public class Roll : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO rollEventChannel;
    [SerializeField] private SkillSOList skillList;
    [SerializeField] private RectTransform cotnentPanel;
    [SerializeField] private float scrollSpeed;
    
    //[SerializeField] private float luck;

    public List<RollItem> rollItems = new List<RollItem>();
    private Dictionary<string, RollDataSO> _skillDic/*k*/ = new Dictionary<string, RollDataSO>();

    private RollDataSO _rolledSkill = null;
    private bool _isRolling = false;
    

    private void Awake()
    {
        foreach(RollDataSO skill in skillList.skillList)
        {
            _skillDic.Add(skill.name, skill);
        }
        
        /*rollItems = GetComponentsInChildren<RollItem>(true).ToList();
        rollItems.ForEach(i => Debug.Log(i.name));*/
        
        rollItems.ForEach(item => item.SettingItem(Rolling()));
    }

    private void Update()
    { 
        cotnentPanel.anchoredPosition = new Vector2(cotnentPanel.anchoredPosition.x - scrollSpeed * Time.deltaTime, cotnentPanel.anchoredPosition.y);

        if (cotnentPanel.anchoredPosition.x <= -215)
        {
            RollItem item = rollItems[0];
            rollItems[0].transform.SetAsLastSibling();
            rollItems[0].SettingItem(Rolling());
            rollItems.RemoveAt(0);
            rollItems.Add(item);
            cotnentPanel.anchoredPosition = Vector2.zero;
        }
    }

    private bool IsPicked(float rarity)
    {
        return Random.Range(1, (int)rarity) == 1;
    }

    public void SkillRoll()
    {
        Rolling();
    }

    private RollDataSO Rolling()
    {
        RollEvent rollEvent = RollEventChannel.RollEvent;

        foreach (RollDataSO skill in _skillDic.Values.Reverse())
        {
            if (IsPicked(skill.rarity / 1))
            {
                return skill;
                rollEvent.rolledSkill = _rolledSkill;
                rollEventChannel.RaiseEvent(rollEvent);
            }
        }


        return null;
    }
}
