    using System;
    using System.Linq;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "SO/Skill/SkillSO", fileName = "SkillSO")]

    public class SkillSO : ScriptableObject
    {
        public string name;
        public int rarity;
        public Sprite icon;
        public string description = string.Empty;
        public Color itemColor;
    }   
