using System;
using DG.DemiLib.Attributes;
using TMPro;
using UnityEngine;

namespace Member.Kmin._01_Script.SkillTree
{
    [DefaultExecutionOrder(100)]
    public class CurrencyViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            CurrencyManager.Instance.OnValueChanged += HandleValueChanged;
        }

        private void OnDestroy()
        {
            CurrencyManager.Instance.OnValueChanged -= HandleValueChanged;
        }

        private void HandleValueChanged(CurrencyType type, int value)
        {
            _text.text = $"{type} : {value}";
        }
    }
}
