using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu (fileName = "SkillSO/SkillList",menuName = "SO/Skill/UseSKillDataSO")]
public class UseSkillDataSO : ScriptableObject
{
    public List<SkillSO> invenSkillList = new List<SkillSO>();
    public List<SkillSO> useSkillList = new List<SkillSO>();
}
