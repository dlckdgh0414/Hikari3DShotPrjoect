using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemNamebg;

    public void SettingItem(RollDataSO rollData)
    {
        gameObject.name = rollData.name;
        itemName.text = rollData.name;
        itemIcon.sprite = rollData.icon;
        itemNamebg.color = rollData.itemColor;
    }
}
