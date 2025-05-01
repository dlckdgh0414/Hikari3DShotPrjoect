using System;
using System.Collections.Generic;
using System.Linq;
using Member.Kmin._01_Script.Core.EventChannel;
using Unity.VisualScripting;
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

        [SerializeField] private List<Transform> childTransform;
        

        private Image _image;
        
        public SkillSO _selectedSkill { get; set; }

        private void Awake()
        {
            _skillInvenEvent.AddListener<SkillSelectEvent>(HandleSkillSelect);
            
            _inventorySO.invenSkillDictionary.Add(tempSO1, true);
            _inventorySO.invenSkillDictionary.Add(tempSO2, true);
            _inventorySO.invenSkillDictionary.Add(tempSO3, true);
        }

        private void Start()
        {
            equipBtns.ForEach(btn => btn.onClick.AddListener(HandleSkillEquip));
        }

        private void HandleSkillEquip()
        {  
            
            foreach (Transform child in childTransform )
            {
                Image img = child.GetComponentInChildren<Image>();
                if (img != null && img.sprite == _selectedSkill.icon)
                {
                    return;
                }
            }
            
            
            _image = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();

            if (_image.sprite == _selectedSkill.icon)
            {
                _image.sprite = null;
                return;
            }
            
            _image.sprite = _selectedSkill.icon;
        }

        private void HandleSkillSelect(SkillSelectEvent evt)
        {
            _selectedSkill = evt.selectedSkill;
        }
    }
}