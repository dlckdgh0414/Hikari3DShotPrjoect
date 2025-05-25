using System.Collections.Generic;
using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.StatSystems;
using UnityEngine;

namespace Member.Kmin._01_Script.SkillTree
{
    public class SaveNodeStat : MonoBehaviour
    {
        public Dictionary<StatSO, float> statData = new Dictionary<StatSO, float>();
        public List<string> skillData;
        public static SaveNodeStat Instance;

        private void Awake()
        {
            statData = new Dictionary<StatSO, float>();
        
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void AddStat(StatSO stat, float value)
        {
            if (!statData.ContainsKey(stat))
            {
                statData.Add(stat, value);
                return;
            }
            
            statData[stat] += value;
        }

        public void LoadStat()
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            EntityStat statCompo = player.GetCompo<EntityStat>();
            SkillCompo skillCompo = player.GetCompo<SkillCompo>();
            
            foreach (var stat in statData.Keys)
            {
                statCompo.RemoveModifier(stat, this);
            }

            foreach (var stat in statData.Keys)
            {
                statCompo.AddModifier(stat, this, statData[stat]);
            }

            foreach (var skill in skillData)
            {
                PassiveSkill target = skillCompo.transform.Find(skill).GetComponent<PassiveSkill>();

                if (target.skillEnabled == false)
                    target.skillEnabled = true;
            }
        }
    }
}
