using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkillSOList", menuName = "Scriptable Objects/SkillSOList")]
public class SkillSOList : ScriptableObject
{
    public List<RollDataSO> skillList;

    private List<RollDataSO> _list = new List<RollDataSO>();

    [ContextMenu("SetByRarity")]
    private void SetByRarity() => skillList.Sort((a, b) => a.rarity.CompareTo(b.rarity));
}
