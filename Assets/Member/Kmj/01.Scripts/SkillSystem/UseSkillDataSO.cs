using System.Collections.Generic;
using Member.Kmin._01_Script.Core.Save;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu (fileName = "SkillSO/SkillList",menuName = "SO/Skill/UseSKillDataSO")]
public class UseSkillDataSO : ScriptableObject
{
    public List<SkillSO> invenSkillList = new List<SkillSO>();
    public List<SkillSO> useSkillList = new List<SkillSO>();
    
    public void Save()
    {
        UseSkillSaveData saveData = new UseSkillSaveData();

        foreach (var skill in invenSkillList)
            saveData.invenSkillID.Add(skill.name);

        foreach (var skill in useSkillList)
            saveData.useSkillID.Add(skill.name);

        SaveLoadManager.Save(saveData);
    }

    public void Load(List<SkillSO> allSkills)
    {
        UseSkillSaveData loadData = SaveLoadManager.Load<UseSkillSaveData>();

        invenSkillList.Clear();
        useSkillList.Clear();

        foreach (string id in loadData.invenSkillID)
        {
            SkillSO skill = allSkills.Find(s => s.name == id);
            if (skill != null)
                invenSkillList.Add(skill);
        }

        foreach (string id in loadData.useSkillID)
        {
            SkillSO skill = allSkills.Find(s => s.name == id);
            if (skill != null)
                useSkillList.Add(skill);
        }
    }
}
