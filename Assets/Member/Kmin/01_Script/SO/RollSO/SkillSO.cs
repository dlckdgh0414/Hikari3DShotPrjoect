using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Skill/SkillSO", fileName = "SkillSO")]

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

    private void OnValidate()
    {
        if (name == string.Empty)
        {
            Debug.Log("³Ê °ª ¾È³ÖÀ½");
        }
        else
        {
            /*Type t = Type.GetType(name);
            Skill skill = Activator.CreateInstance(t) as Skill;
            ThisSkill = skill;*/
        }
    }
}
