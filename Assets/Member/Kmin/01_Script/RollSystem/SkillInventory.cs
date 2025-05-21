using System.Collections.Generic;
using UnityEngine;

//namespace Member.Kmin._01_Script.RollSystem
//{
//    public class SkillInventory : MonoBehaviour
//    {
//        [SerializeField] private GameEventChannelSO eventChannel;
//        public List<SkillSO> _ownSkillList { get; private set; }
//            = new List<SkillSO>();
//
//        private void Awake()
//        {
//            eventChannel.AddListener<RollEndEvent>(HandleRollEnd);
//        }
//
//        private void HandleRollEnd(RollEndEvent evt)
//        {
//            SkillSO rolledSkill = evt.rolledSkill;
//
//            if (!_ownSkillList.Contains(rolledSkill))
//            {
//                _ownSkillList.Add(rolledSkill);
//            }
//            else
//            {
//                CurrencyManager.Instance.ModifyCurrency(CurrencyType.Eon, ModifyType.Add, 100);
//            }
//
//            foreach (var skill in _ownSkillList)
//            {
//                //Debug.Log(skill);
//            }
//        }
//    }
//}
