using System;
using System.Collections.Generic;
using System.Linq;
using Member.Kmin._01_Script.Core.EventChannel;
using Member.Kmj._01.Scripts.Core.EventChannel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Member.Kmj._01.Scripts.UI.Inventory
{
    public class SkillInventory : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _skillSendEvent;
        
        private SendSkill _skillEvent = SendSkillChannel.SkillEquipEvent;
        
        private SendStaticSkill _staticSkilEvent = SendSkillChannel.staticSkillEquipEvent;
        
        
        
        [SerializeField] private GameEventChannelSO _skillInvenEvent;
        [SerializeField] private UseSkillDataSO _inventorySO;
        [SerializeField] private List<Button> equipBtns;
        [SerializeField] private Button _staticButton;

        [SerializeField] private SkillSO tempSO1;
        [SerializeField] private SkillSO tempSO2;
        [SerializeField] private SkillSO tempSO3;

        [SerializeField] private List<Transform> childTransform;
        

        private Image _image;
        
        public SkillSO _selectedSkill { get; set; }
        public SkillSO _staticSkill { get; set; }

        private void Awake()
        {
            _skillInvenEvent.AddListener<SkillSelectEvent>(HandleSkillSelect);
            _skillInvenEvent.AddListener<StaticSelectEvent>(HandleStaticSkillSelect);
            _inventorySO.invenSkillList.Add(tempSO1);
            _inventorySO.invenSkillList.Add(tempSO2);
            _inventorySO.invenSkillList.Add(tempSO3);
        }

        private void Start()
        {
            _staticButton.onClick.AddListener(HandleStaticSkillEquip);
            equipBtns.ForEach(btn => btn.onClick.AddListener(HandleSkillEquip));
        }

        private void HandleSkillEquip()
        {  
            if (_selectedSkill == null || _selectedSkill.icon == null)
                return;
            
            if (EventSystem.current.currentSelectedGameObject == null)
                return;

            Image clickedImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
            if (clickedImage == null)
                return;
            
            foreach (Transform child in childTransform)
            {
                Image img = child.GetComponentInChildren<Image>();
                if (img != null && img.sprite == _selectedSkill.icon)
                {
                    clickedImage.sprite = _selectedSkill.icon;
                    img.sprite = null;
                }
            }
            
            clickedImage.sprite = _selectedSkill.icon;
            
            clickedImage = null;
            _selectedSkill = null;
            
            _skillEvent.selectedSkill = _selectedSkill.name;
            _skillSendEvent.RaiseEvent(_skillEvent);
        }
        
        private void HandleStaticSkillEquip()
        {  
            if (_staticSkill == null || _staticSkill.icon == null)
                return;
            
            if (EventSystem.current.currentSelectedGameObject == null)
                return;

            Image clickedImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
            if (clickedImage == null)
                return;
            
            clickedImage.sprite = _staticSkill.icon;
            _staticSkilEvent.staticSkill = _staticSkill.name;
            _skillSendEvent.RaiseEvent(_skillEvent);
            
            clickedImage = null;
            _staticSkill = null;
            
        }
        

        private void HandleSkillSelect(SkillSelectEvent evt)
        {
            _selectedSkill = evt.selectedSkill;
        }
        
        private void HandleStaticSkillSelect(StaticSelectEvent evt)
        {
            _staticSkill = evt.staticSkill;
        }
        
    }
}