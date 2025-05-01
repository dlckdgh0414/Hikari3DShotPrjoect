using System;
using System.Collections.Generic;
using Member.Kmin._01_Script.Core.EventChannel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Member.Kmj._01.Scripts.UI.Inventory
{
    public class SkillInventory : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _skillInvenEvent;
        [SerializeField] private SkillInventorySO _inventorySO;
        [SerializeField] private List<SkillSO> _skills;
        [SerializeField] private List<Button> equipBtns;

        private Image _image;
        
        private SkillSO _selectedSkill;

        private void Awake()
        {
            _skillInvenEvent.AddListener<SkillSelectEvent>(HandleSkillSelect);
            
            equipBtns.ForEach(btn => btn.onClick.AddListener(HandleSkillEquip));
        }

        private void HandleSkillEquip()
        {
            _image = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
            _image.sprite = _selectedSkill.icon;
        }

        private void HandleSkillSelect(SkillSelectEvent evt)
        {
            _selectedSkill = evt.selectedSkill;
        }
    }
}