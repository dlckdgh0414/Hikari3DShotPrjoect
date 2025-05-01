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
        [SerializeField] private UseSkillDataSO _inventorySO;
        [SerializeField] private List<Button> equipBtns;

        [SerializeField] private SkillSO tempSO1;
        [SerializeField] private SkillSO tempSO2;
        [SerializeField] private SkillSO tempSO3;

        private Image _image;
        
        private SkillSO _selectedSkill;

        private void Awake()
        {
            _skillInvenEvent.AddListener<SkillSelectEvent>(HandleSkillSelect);
            
            equipBtns.ForEach(btn => btn.onClick.AddListener(HandleSkillEquip));
            
            _inventorySO.invenSkillDictionary.Add(tempSO1, true);
            _inventorySO.invenSkillDictionary.Add(tempSO2, true);
            _inventorySO.invenSkillDictionary.Add(tempSO3, true);
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