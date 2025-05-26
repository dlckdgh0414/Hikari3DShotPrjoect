using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Member.Kmin._06_SO.Skin;
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
    [SerializeField] private PlayerSkinSOList playerSkinSO;
    [SerializeField] private UseSkillDataSO skillData;
    [SerializeField] private GameObject background;
    [SerializeField] private Image rolledSkillBackground;
    public List<RollItem> rollItems = new List<RollItem>();

    [Header("------------------------Setting------------------------")]
    [SerializeField] private int price;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float luck;
    
    private Dictionary<string, PlayerSkinSO> _skinDic = new Dictionary<string, PlayerSkinSO>();
    
    private readonly RollEndEvent _rollEndEvent = new RollEndEvent();

    private float _scrollSpeed;
    private bool _isRolling = false;
    
    private void Awake()
    {
        rolledSkillText.transform.parent.gameObject.SetActive(false);

        playerSkinSO.skinList.ForEach(s =>  _skinDic.Add(s.name, s));
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
        if (_isRolling) return;

        if (CurrencyManager.Instance.GetCurrency(CurrencyType.Eon) < price) return;
        
        CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, -price);

        DOTween.To(() => 0f, y => maskBackground.rectTransform.sizeDelta = 
            new Vector2(maskBackground.rectTransform.sizeDelta.x, y), 600f, 2.25f)
            .SetEase(Ease.InExpo).OnComplete(() => DecreaseRollingSpeed());
        
        rolledSkillText.transform.parent.gameObject.SetActive(false);
        _scrollSpeed = scrollSpeed;
        _isRolling = true;
    }

    private void DecreaseRollingSpeed()
    {
        DOTween.To(() => _scrollSpeed, x => _scrollSpeed = x, 0, 6.5f)
            .SetEase(Ease.OutCirc);
    }

    private void Rolling()
    {
        contentPanel.anchoredPosition += Vector2.left * (_scrollSpeed * Time.deltaTime);

        if (_scrollSpeed <= 25)
        {
            RollEnd();
            DOTween.To(() => 300f, y => maskBackground.rectTransform.sizeDelta =
                new Vector2(maskBackground.rectTransform.sizeDelta.x, y), 0f, 2f).SetEase(Ease.InExpo);
        }

        if (contentPanel.anchoredPosition.x <= -240)
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

        PlayerSkinSO rolledSkin = _skinDic
            .Where(x => x.Key == rolledName)
            .Select(x => x.Value)
            .FirstOrDefault();

        if (skillData.invenSkillList.Contains(rolledSkin) == false)
        {
            skillData.invenSkillList.Add(rolledSkin);
        }
        else
        {
            CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, price / 2);
        } 
        
        rolledSkillText.transform.parent.gameObject.SetActive(true);
        rolledSkillText.text = $"{rolledSkin.name}({rolledSkin.rarity}분의 1)";
        rolledSkillBackground.color = rolledSkin.itemColor;
        
        _isRolling = false;
        _rollEndEvent.rolledSkill = rolledSkin;
        rollEventChannel.RaiseEvent(_rollEndEvent);
    }

    private PlayerSkinSO SelectedSkill()
    {
        RollStartEvent rollStartEvent = RollEventChannel.rollStartEvent;

        foreach (PlayerSkinSO skin in _skinDic.Values.Reverse())
        {
            if (IsPicked(skin.rarity / 1 * luck))
            {
                return skin;
            }
        }

        return null;
    }
    
    public void ChangeActive() => background.SetActive(!background.activeSelf);
}
