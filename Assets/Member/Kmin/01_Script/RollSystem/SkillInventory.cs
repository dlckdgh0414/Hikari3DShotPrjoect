using System.Collections.Generic;
using UnityEngine;

namespace Member.Kmin._01_Script.RollSystem
{
    public class SkillInventory : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannel;
        public Dictionary<SkillSO, int> _ownSkillDic { get; private set; }
            = new Dictionary<SkillSO, int>();

        private void Awake()
        {
            eventChannel.AddListener<RollEndEvent>(HandleRollEnd);
        }

        private void HandleRollEnd(RollEndEvent evt)
        {
            SkillSO rolledSkill = evt.rolledSkill;

            if (!_ownSkillDic.ContainsKey(rolledSkill))
            {
                _ownSkillDic.Add(rolledSkill, 1);
            }
            else
            {
                _ownSkillDic[rolledSkill]++;
                CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, 100);
            }

            foreach (var skill in _ownSkillDic)
            {
                //Debug.Log(skill);
            }
        }
    }
}
