using System.Linq;
using Member.Ysc._01_Code.StatSystems;
using UnityEngine;

namespace Member.Ysc._01_Code.Agent
{
    public class EntityStat : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private StatOverride[] statOverrideArray;
        private StatSO[] _stat;
        
        public Entity Owner { get; private set; }

        public void Initialize(Entity entity)
        {
            Owner = entity;
            _stat = statOverrideArray.Select(stat => stat.CreateStat()).ToArray();
        }

        public StatSO GetStat(StatSO targetStat)
        {
            Debug.Assert(targetStat != null, "Stats::GetStat : targetStat is null");
            return _stat.FirstOrDefault(stat => stat.statName == targetStat.statName);
        }

        public bool TryGetStat(StatSO targetStat, out StatSO outStat)
        {
            Debug.Assert(targetStat != null, "Stats::TryGetStat : targetStat is null");
            
            outStat = _stat.FirstOrDefault(stat => stat.statName == targetStat.statName);
            return outStat;

        }

        public void SetBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue = value;
        public float GetBaseValue(StatSO stat) => GetStat(stat).BaseValue;
        public void IncreaseBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue += value;
        public void AddModifier(StatSO stat, object key, float value) => GetStat(stat).AddModifier(key, value);
        public void RemoveModifier(StatSO stat, object key) => GetStat(stat).RemoveModifier(key);
    }
}