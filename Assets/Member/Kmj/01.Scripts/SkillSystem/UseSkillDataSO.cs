using System.Collections.Generic;
using Member.Kmin._01_Script.Core.Save;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu (fileName = "SkillSO/SkillList",menuName = "SO/Skill/UseSKillDataSO")]
public class UseSkillDataSO : ScriptableObject
{
    public List<PlayerSkinSO> invenSkillList = new List<PlayerSkinSO>();
    public List<PlayerSkinSO> useSkillList = new List<PlayerSkinSO>();
    
    public void Save()
    {
        UseSkillSaveData saveData = new UseSkillSaveData();

        foreach (var skill in invenSkillList)
            saveData.invenSkillID.Add(skill.name);

        foreach (var skill in useSkillList)
            saveData.useSkillID.Add(skill.name);

        SaveLoadManager.Save(saveData);
    }

    public void Load(List<PlayerSkinSO> allSkills)
    {
        UseSkillSaveData loadData = SaveLoadManager.Load<UseSkillSaveData>();

        invenSkillList.Clear();
        useSkillList.Clear();

        foreach (string id in loadData.invenSkillID)
        {
            PlayerSkinSO skin = allSkills.Find(s => s.name == id);
            if (skin != null)
                invenSkillList.Add(skin);
        }

        foreach (string id in loadData.useSkillID)
        {
            PlayerSkinSO skin = allSkills.Find(s => s.name == id);
            if (skin != null)
                useSkillList.Add(skin);
        }
    }
}
