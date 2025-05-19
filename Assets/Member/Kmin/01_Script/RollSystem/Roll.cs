using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{
    [Header("------------------------Assignment------------------------")]
    [SerializeField] private GameEventChannelSO rollEventChannel;
    [SerializeField] private Image maskBackground;
    [SerializeField] private TextMeshProUGUI rolledSkillText;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private SkillSOList skillListSO;
    [SerializeField] private UseSkillDataSO skillData;
    public List<RollItem> rollItems = new List<RollItem>();

    [Header("------------------------Setting------------------------")]
    [SerializeField] private int price;
    [SerializeField] private float scrollSpeed;
    
    private Dictionary<string, SkillSO> _skillDic/*k*/ = new Dictionary<string, SkillSO>();
    
    private readonly RollEndEvent _rollEndEvent = new RollEndEvent();

    private float _scrollSpeed;
    private bool _isRolling = false;
    private bool _isDecrease = false;
    
    private void Awake()
    {
        rolledSkillText.transform.parent.gameObject.SetActive(false);

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

    #if UNITY_EDITOR
    [ContextMenu("Roll")]
    #endif
    public void SkillRoll()
    {
        if (CurrencyManager.Instance.GetCurrency(CurrencyType.Eon) < price) return;
        
        CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, -price);

        DOTween.To(() => 0f, y => maskBackground.rectTransform.sizeDelta = 
            new Vector2(maskBackground.rectTransform.sizeDelta.x, y), 600f, 2.25f)
            .SetEase(Ease.InExpo).OnComplete(() => _isDecrease = true);
        
        rolledSkillText.transform.parent.gameObject.SetActive(false);
        _scrollSpeed = scrollSpeed;
        _isRolling = true;
        
        Debug.Log("dkdkdkadshla;hla");
    }

    private void Rolling()
    {
        contentPanel.anchoredPosition += Vector2.left * (_scrollSpeed * Time.deltaTime);
        if(_isDecrease) _scrollSpeed /= (1.005f);

        if (_scrollSpeed <= 25)
        {
            RollEnd();
            DOTween.To(() => 300f, y => maskBackground.rectTransform.sizeDelta =
                new Vector2(maskBackground.rectTransform.sizeDelta.x, y), 0f, 2f).SetEase(Ease.InExpo);
        }

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
        maskBackground.rectTransform.sizeDelta = new Vector2(maskBackground.rectTransform.sizeDelta.x, 0);
        
        string rolledName = rollItems.OrderBy(x => 
            Vector3.Distance(contentPanel.parent.position, x.gameObject.transform.position)).First().name;

        SkillSO rolledSkill = _skillDic
            .Where(x => x.Key == rolledName)
            .Select(x => x.Value)
            .FirstOrDefault();

        if (skillData.invenSkillList.Contains(rolledSkill) == false)
        {
            skillData.invenSkillList.Add(rolledSkill);
        }
        else
        {
            CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, price / 2);
        } 
        
        rolledSkillText.transform.parent.gameObject.SetActive(true);
        rolledSkillText.text = $"{rolledSkill.name}({rolledSkill.rarity}분의 1)";
        
        _isRolling = false;
        _isDecrease = false;
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
