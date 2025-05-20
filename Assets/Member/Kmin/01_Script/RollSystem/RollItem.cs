using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemNamebg;

    public void SettingItem(PlayerSkinSO skillSO)
    {
        gameObject.name = skillSO.name;
        itemName.text = skillSO.name;
        itemIcon.sprite = skillSO.icon;
        itemNamebg.color = skillSO.itemColor;
    }
}
