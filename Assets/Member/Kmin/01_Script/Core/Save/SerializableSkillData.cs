using System.Collections.Generic;

[System.Serializable]
public class SerializableSkillData
{
    public List<SkillEntry> invenSkillList = new List<SkillEntry>();
    public List<SkillSO> useSkillList = new List<SkillSO>();
}

[System.Serializable]
public class SkillEntry
{
    public SkillSO skill;
    public bool isUnlocked;
}