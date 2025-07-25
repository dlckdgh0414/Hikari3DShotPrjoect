using System;
using System.Collections.Generic;
using Member.Kmin._01_Script.Core.Save;
using Member.Kmin._01_Script.SkillTree;
using Member.Ysc._01_Code.StatSystems;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NodeSO", menuName = "SO/NodeSO")]
public class NodeSO : ScriptableObject
{
    public Sprite icon;
    public string nodeName;
    public int price;
    public float upgradeValue;
    public string passiveSkill;
    public StatSO statSO;
    [TextArea] public string description;
    public bool isPurchase = false;
    public bool isActive {get; set;}
    public SkillTreeNode SkillTreeNode { get; set; }
}