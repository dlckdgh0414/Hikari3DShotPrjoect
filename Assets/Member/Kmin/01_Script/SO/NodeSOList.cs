using System.Collections.Generic;
using Member.Kmin._01_Script.Core.Save;
using UnityEngine;

namespace Member.Kmin._01_Script.SO
{
    [CreateAssetMenu(fileName = "NodeSOList", menuName = "SO/NodeSOList", order = 0)]
    public class NodeSOList : ScriptableObject
    {
        public List<NodeSO> nodeSOList = new List<NodeSO>();
        
        public void Save()
        {
            NodeSoSaveData saveData = new NodeSoSaveData();

            foreach (var skill in nodeSOList)
                saveData.nodeSOID.Add(skill.name);
            
            SaveLoadManager.Save(saveData);
        }

        public void Load(List<NodeSO> allSkills)
        {
            NodeSoSaveData loadData = SaveLoadManager.Load<NodeSoSaveData>();
            nodeSOList.Clear();

            if (loadData == null) return;

            foreach (string id in loadData.nodeSOID)
            {
                NodeSO skin = allSkills.Find(s => s.name == id);
                if (skin != null)
                    nodeSOList.Add(skin);
            }
        }
    }
}