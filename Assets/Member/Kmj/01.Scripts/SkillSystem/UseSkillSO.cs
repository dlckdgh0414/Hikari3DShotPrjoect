using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SkillSO/SkillList",menuName = "SkillSO/SkillList")]
public class UseSkillSO : ScriptableObject
{
    public Dictionary<string, SkillSO> UseSkillDictionary;
}
