using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillLIst/skillLIst", menuName = "SkillList")]

public class SkillInventorySO : ScriptableObject
{
    public Dictionary<string, SkillSO> normalSkillList;

    public Dictionary<string, SkillSO> staticSkillList;

    public Dictionary<string, SkillSO> TotalSkillList;
    public void AddNormalSkill(string name, SkillSO skill)
    {
        normalSkillList.Add(name, skill);

        GameObject thisSkill = GameObject.Find(skill.skillName);

        thisSkill.GetComponent<Button>().interactable = true;

        TotalSkillList.Add(skill.skillName, skill);
    }

    public void AddStaticSkill(string name, SkillSO skill)
    {
        staticSkillList.Add(name, skill);

        GameObject thisSkill = GameObject.Find(skill.skillName);

        thisSkill.GetComponent<Button>().interactable = true;
        TotalSkillList.Add(skill.skillName,skill);

    }


    public void RemoveNormalSkill(string name, SkillSO skill)
    {
        normalSkillList.Remove(name);

        GameObject thisSkill = GameObject.Find(skill.skillName);

        thisSkill.GetComponent<Button>().interactable = false;

        TotalSkillList.Remove(skill.skillName);
    }

    public void RemoveStaticSkill(string name, SkillSO skill)
    {
        staticSkillList.Remove(name);

        GameObject thisSkill = GameObject.Find(skill.skillName);

        thisSkill.GetComponent<Button>().interactable = false;

        TotalSkillList.Remove(skill.skillName);
    }

}
