using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SkillSO/SkillList",menuName = "SkillSO/SkillList")]
public class UseSkillDataSO : ScriptableObject
{
    public Dictionary<SkillSO, bool> invenSkillDictionary = new Dictionary<SkillSO, bool>();
    public List<SkillSO> useSkillDictionary = new List<SkillSO>();
}
