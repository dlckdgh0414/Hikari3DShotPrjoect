using System;
using System.Collections.Generic;
using UnityEngine;

namespace Member.Ysc._01_Code.StatSystems
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSystem/Stat", order = 0)]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValueChangeHandler(StatSO stat, float current, float previous);
        
        public event ValueChangeHandler OnValueChange;

        public string statName;
        [TextArea] public string description;

        [SerializeField] private Sprite icon;
        [SerializeField] private string displayName;
        [SerializeField] private float baseValue, minValue, maxValue;
        
        private Dictionary<object, float> _modifyDictionary = new Dictionary<object, float>();
        
        [field: SerializeField] public bool IsPercent { get; private set; }

        private float _modifyValue = 0;
        
        #region Property Section
        
        public Sprite Icon => icon;

        public float MaxValue
        {
            get => maxValue;
            set => maxValue = value;
        }

        public float MinValue
        {
            get => minValue;
            set => minValue = value;
        }
        
        public float Value => Mathf.Clamp(baseValue + _modifyValue, minValue, maxValue);
        public bool IsMax => Mathf.Approximately(Value, MaxValue);
        public bool IsMin => Mathf.Approximately(Value, MinValue);

        public float BaseValue
        {
            get => baseValue;
            set
            {
                float prevValue = Value;
                baseValue = Mathf.Clamp(value, minValue, maxValue);
                TryInvokeValueChangedEvent(Value, prevValue);
            }
        }
        
        #endregion

        public void AddModifier(object key, float value)
        {
            if (_modifyDictionary.ContainsKey(key)) return;
            
            float prevValue = Value;
            
            _modifyValue += value;
            _modifyDictionary.Add(key, value);
            
            TryInvokeValueChangedEvent(Value, prevValue);
        }

        public void RemoveModifier(object key)
        {
            if (_modifyDictionary.TryGetValue(key, out float vlaue))
            {
                float prevValue = Value;
                _modifyValue -= vlaue;
                _modifyDictionary.Remove(key);
                
                TryInvokeValueChangedEvent(Value, prevValue);
            }
        }

        public void ClearModifier()
        {
            float prevValue = Value;
            _modifyDictionary.Clear();
            _modifyValue = 0;
            TryInvokeValueChangedEvent(Value, prevValue);
        }

        private void TryInvokeValueChangedEvent(float current, float prevValue)
        {
            if (Mathf.Approximately(current, prevValue) == false)
                OnValueChange?.Invoke(this, current, prevValue);
        }

        public object Clone() => Instantiate(this);
    }
}