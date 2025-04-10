using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeTooltip : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO eventChannel;

    private Button _purchaseBtn;
    private Transform _background;
    private Transform _textArea;
    private TextMeshProUGUI _description;
    private TextMeshProUGUI _fruitsPrice;
    private TextMeshProUGUI _fruitsName;
    private Image _icon;

    private void Awake()
    {
        eventChannel.AddListener<SkillTreeEvent>(HandleOnFruitsSelect);

        _background = transform.Find("Background");
        _icon = _background.Find("FruitsIcon/Icon").GetComponent<Image>();
        _textArea = _background.Find("TextArea");

        _description = _textArea.Find("Description").GetComponent<TextMeshProUGUI>();
        _fruitsPrice = _textArea.Find("FruitsPrice").GetComponent<TextMeshProUGUI>();
        _fruitsName = _textArea.Find("FruitsName").GetComponent<TextMeshProUGUI>();

        _purchaseBtn = _background.GetComponentInChildren<Button>();
    }

    private void HandleOnFruitsSelect(SkillTreeEvent skillTreeEvent)
    {
        _description.text = skillTreeEvent.fruitsSO.description;
        _fruitsPrice.text = skillTreeEvent.fruitsSO.price.ToString();
        _fruitsName.text = skillTreeEvent.fruitsSO.fruitsName;
        _icon.sprite = skillTreeEvent.fruitsSO.icon;

        _purchaseBtn.onClick.RemoveAllListeners();
        _purchaseBtn.onClick.AddListener(() => HandleFruitsPurchase(skillTreeEvent));

        Debug.Log("sdf");
    }

    private void HandleFruitsPurchase(SkillTreeEvent skillTreeEvent) => skillTreeEvent.fruitsSO.Fruits.PurchaseFruits();
}
