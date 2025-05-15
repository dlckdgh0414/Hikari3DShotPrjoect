using System;
using Member.Kmin._01_Script.SkillTree;
using Member.Ysc._01_Code.StatSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeTooltip : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO eventChannel;
    [SerializeField] private SkillTree skillTree;
    [SerializeField] private GameObject background;
    
    private SkillTreeSO skillTreeSO;

    private Button _purchaseBtn;
    private Transform _textArea;
    private TextMeshProUGUI _description;
    private TextMeshProUGUI _priceText;
    private TextMeshProUGUI _nodeName;
    private TextMeshProUGUI _purchaseText;
    private Image _icon;
    
    private SkillTreePurchaseEvent _treePurchaseEvent = SkillTreeEventChannel.SkillTreePurchaseEvent;

    private void Awake()
    {
        eventChannel.AddListener<SkillTreeSelectEvent>(HandleOnFruitsSelect);
        
        _purchaseBtn = background.GetComponentInChildren<Button>();
        _purchaseText = _purchaseBtn.GetComponentInChildren<TextMeshProUGUI>();
        _description = background.transform.Find("TextArea").GetComponentInChildren<TextMeshProUGUI>();
        _nodeName = background.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        _priceText = background.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        _icon = background.transform.Find("Icon").GetComponent<Image>();
        
        skillTreeSO = skillTree.skillTreeSO;
    }

    private void HandleOnFruitsSelect(SkillTreeSelectEvent evt)
    {
        NodeSO node = evt.node.GetNodeSO();

        _purchaseText.text = node.isPurchase ? "소유중" : "구매하기";
        _icon.sprite = node.SkillSO ==null ? node.statSO.Icon : node.SkillSO.icon;

        _description.text = node.description;
        _priceText.text = $"{node.price}원";
        _nodeName.text = node.nodeName;

        _purchaseBtn.onClick.RemoveAllListeners();
        _purchaseBtn.onClick.AddListener(() => HandleFruitsPurchase(evt.node));
    }

    private void HandleFruitsPurchase(SkillTreeNode node)
    {
        if (node.GetNodeSO().isPurchase) return;
        
        _purchaseText.text = "소유중";
        _treePurchaseEvent.node = node;
        eventChannel.RaiseEvent(_treePurchaseEvent);
    }
}
