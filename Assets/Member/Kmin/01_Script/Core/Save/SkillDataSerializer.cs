using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SkillDataSerializer
{
    public static void SaveToJson(UseSkillDataSO skillData, string path)
    {
        SerializableSkillData serializableData = new SerializableSkillData();

        foreach (var kvp in skillData.invenSkillDictionary)
        {
            serializableData.invenSkillList.Add(new SkillEntry { skill = kvp.Key, isUnlocked = kvp.Value });
        }

        serializableData.useSkillList = skillData.useSkillDictionary;

        string json = JsonUtility.ToJson(serializableData, true);
        File.WriteAllText(path, json);
        Debug.Log($"Skill data saved to {path}");
    }

    public static void LoadFromJson(UseSkillDataSO skillData, string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning($"File not found at {path}");
            return;
        }

        string json = File.ReadAllText(path);
        SerializableSkillData loadedData = JsonUtility.FromJson<SerializableSkillData>(json);

        skillData.invenSkillDictionary.Clear();
        foreach (var entry in loadedData.invenSkillList)
        {
            skillData.invenSkillDictionary[entry.skill] = entry.isUnlocked;
        }

        skillData.useSkillDictionary = new List<SkillSO>(loadedData.useSkillList);
        Debug.Log($"Skill data loaded from {path}");
    }
}