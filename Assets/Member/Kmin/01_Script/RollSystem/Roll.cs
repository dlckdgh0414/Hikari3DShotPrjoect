using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

public class Roll : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO rollEventChannel;
    [SerializeField] private SkillSOList skillListSO;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private float scrollSpeed;
    [FormerlySerializedAs("skillSO")] [SerializeField] private UseSkillDataSO skillDataSo;

    public List<RollItem> rollItems = new List<RollItem>();
    private Dictionary<string, SkillSO> _skillDic/*k*/ = new Dictionary<string, SkillSO>();
    
    private readonly RollEndEvent _rollEndEvent = new RollEndEvent();

    private float _scrollSpeed;
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
    public void SkillRoll()
    {
        _scrollSpeed = scrollSpeed;
        _isRolling = true;
    }

    private void Rolling()
    {
        contentPanel.anchoredPosition += Vector2.left * (_scrollSpeed * Time.deltaTime);
        _scrollSpeed /= (1.005f);
 
        if (_scrollSpeed <= 25) RollEnd();

        if (contentPanel.anchoredPosition.x <= -215)
        {
            RollItem item = rollItems[0];
            rollItems[0].transform.SetAsLastSibling();
            rollItems[0].SettingItem(SelectedSkill());
            
            rollItems.RemoveAt(0);
            rollItems.Add(item);
            contentPanel.anchoredPosition = Vector2.zero;
        }
    }

    private void RollEnd()
    {
        _scrollSpeed = 0;

        string rolledName = rollItems.OrderBy(x => 
            Vector3.Distance(contentPanel.parent.position, x.gameObject.transform.position)).First().name;

        SkillSO rolledSkill = _skillDic
            .Where(x => x.Key == rolledName)
            .Select(x => x.Value)
            .FirstOrDefault();

        if (skillDataSo.UseSkillDictionary.ContainsKey(rolledSkill))
        {
            skillDataSo.UseSkillDictionary[rolledSkill]++;
        }
        else
        {
            skillDataSo.UseSkillDictionary.Add(rolledSkill, 1);
        }
        
        _isRolling = false;
        _rollEndEvent.rolledSkill = rolledSkill;
        rollEventChannel.RaiseEvent(_rollEndEvent);
    }

    private SkillSO SelectedSkill()
    {
        RollStartEvent rollStartEvent = RollEventChannel.rollStartEvent;

        foreach (SkillSO skill in _skillDic.Values.Reverse())
        {
            if (IsPicked(skill.rarity / 1))
            {
                return skill;
            }
        }

        return null;
    }
}
