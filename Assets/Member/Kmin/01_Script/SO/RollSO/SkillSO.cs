using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Skill/So", fileName = "SKills")]

public class SkillSO : ScriptableObject
{
    public string name;
    public int rarity;
    public Sprite icon;
    [TextArea]
    public string description = string.Empty;
    public Color itemColor;
    public TextMeshProUGUI skillText;
    
    public float skillDamage;
    public float skillCoolTime;
    public float currentcoolTime;
}
