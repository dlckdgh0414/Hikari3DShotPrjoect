using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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

    public Skill ThisSkill;
}
