using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SkillSO/SkillList",menuName = "SkillSO/SkillList")]
public class UseSkillDataSO : ScriptableObject
{
    public Dictionary<SkillSO, int> UseSkillDictionary = new Dictionary<SkillSO, int>();
}
