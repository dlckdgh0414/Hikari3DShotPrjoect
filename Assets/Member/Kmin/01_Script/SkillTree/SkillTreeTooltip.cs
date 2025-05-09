using System;
using Member.Ysc._01_Code.StatSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeTooltip : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO eventChannel;
    [SerializeField] private SkillTree skillTree;
    [SerializeField] private StatSO selectNodeCount;
    
    private SkillTreeSO skillTreeSO;

    private Button _purchaseBtn;
    private Transform _background;
    private Transform _textArea;
    private Transform _purchaseArea;
    private TextMeshProUGUI _description;
    private TextMeshProUGUI _fruitsPrice;
    private TextMeshProUGUI _fruitsName;
    private TextMeshProUGUI _purchaseText;
    private Image _icon;
    
    private SkillTreePurchaseEvent _treePurchaseEvent = SkillTreeEventChannel.SkillTreePurchaseEvent;
    private SkillTreeActiveEvent _treeActiveEvent = SkillTreeEventChannel.SkillTreeActiveEvent;

    private void Awake()
    {
        eventChannel.AddListener<SkillTreeSelectEvent>(HandleOnFruitsSelect);

        _background = transform.Find("Background");
        _icon = _background.Find("FruitsIcon/Icon").GetComponent<Image>();
        _textArea = _background.Find("TextArea");
        _purchaseArea = _background.Find("PurchaseArea");
        _description = _textArea.Find("Description").GetComponent<TextMeshProUGUI>();
        _fruitsName = _background.Find("Name").GetComponent<TextMeshProUGUI>();
        _fruitsPrice = _purchaseArea.Find("FruitsPrice").GetComponent<TextMeshProUGUI>();
        
        _purchaseBtn = _background.GetComponentInChildren<Button>();
        _purchaseText = _purchaseBtn.GetComponentInChildren<TextMeshProUGUI>();

        skillTreeSO = skillTree.skillTreeSO;
    }

    private void HandleOnFruitsSelect(SkillTreeSelectEvent evt)
    {
        NodeSO node = evt.node.GetNodeSO();

        _description.text = node.SkillSO.description;
        _fruitsPrice.text = node.price.ToString();
        _fruitsName.text = node.nodeName;
        _icon.sprite = node.SkillSO.icon;

        _purchaseBtn.onClick.RemoveAllListeners();
        _purchaseText.text = "Purchased";
        _purchaseBtn.onClick.AddListener(() => HandleFruitsPurchase(evt.node));
    }

    private void HandleFruitsPurchase(SkillTreeNode node)
    {
        node.GetNodeSO().isPurchase = true;
        _treePurchaseEvent.node = node;
        eventChannel.RaiseEvent(_treePurchaseEvent);
    }
}
