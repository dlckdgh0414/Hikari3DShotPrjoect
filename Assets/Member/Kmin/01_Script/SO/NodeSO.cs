using System;
using System.Collections.Generic;
using Member.Kmin._01_Script.SkillTree;
using Member.Ysc._01_Code.StatSystems;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NodeSO", menuName = "SO/NodeSO")]
public class NodeSO : ScriptableObject
{
    public string nodeName;
    public int price;
    public float upgradeValue;
    public SkillSO SkillSO;
    public StatSO statSO;
    [TextArea] public string description;
    public bool isPurchase { get; set; } = false;
    public bool isActive {get; set;}
    public SkillTreeNode SkillTreeNode { get; set; }

    private void OnValidate()
    {
        nodeName = this.name;
    }
}