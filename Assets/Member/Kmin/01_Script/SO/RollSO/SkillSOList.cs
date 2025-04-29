using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkillSOList", menuName = "Scriptable Objects/SkillSOList")]
public class SkillSOList : ScriptableObject
{
    public List<SkillSO> skillList;

    private List<SkillSO> _list = new List<SkillSO>();

    [ContextMenu("SetByRarity")]
    private void SetByRarity() => skillList.Sort((a, b) => a.rarity.CompareTo(b.rarity));
}
