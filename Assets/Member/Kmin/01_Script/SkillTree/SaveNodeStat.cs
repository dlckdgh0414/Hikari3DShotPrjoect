using System.Collections.Generic;
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

        [ContextMenu("LOG")]
        private void Test()
        {
            foreach (var data in statData)
            {
                Debug.Log(data.Key + " : " + data.Value);
            }
        }
    }
}
