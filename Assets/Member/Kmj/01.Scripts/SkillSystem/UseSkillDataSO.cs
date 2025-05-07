using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu (fileName = "SkillSO/SkillList",menuName = "SO/Skill/SkillList")]
public class UseSkillDataSO : ScriptableObject
{
    public Dictionary<SkillSO, bool> invenSkillDictionary = new Dictionary<SkillSO, bool>();
    public List<SkillSO> useSkillList = new List<SkillSO>();
}
