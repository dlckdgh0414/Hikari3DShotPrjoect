using System;
using System.Collections.Generic;
using System.Linq;
using Member.Kmin._01_Script.Core.EventChannel;
using Member.Kmj._01.Scripts.Core.EventChannel;
using Unity.VisualScripting;
using UnityEditor;
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

        [SerializeField] private Sprite _baseImage;
        
        
        [SerializeField] private GameEventChannelSO _skillInvenEvent;
        [SerializeField] private UseSkillDataSO _inventorySO;
        [SerializeField] private List<Button> equipBtns;
        [SerializeField] private Button _staticButton;

        [SerializeField] private SkillSO tempSO1;
        [SerializeField] private SkillSO tempSO2;
        [SerializeField] private SkillSO tempSO3;

        [SerializeField] private List<Transform> childTransform;

        [SerializeField] private Transform _skillTransform;
        

        private Image _image;
        
        public SkillSO _selectedSkill { get; set; }
        

        private string path;

        private void Awake()
        {
            _skillInvenEvent.AddListener<SkillSelectEvent>(HandleSkillSelect);
            
        }

        private void Start()
        {
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

                    img.sprite = _baseImage;
                    
                    print("삭제됨");
                    if (child.name.Contains("2"))
                    {
                        PlayerSendInfo.Instance.skillName[0] = string.Empty;
                        child.GetComponent<EqumentBtn>()._thisSkill = null;
                    }
                    else if (child.name.Contains("3"))
                    {
                        PlayerSendInfo.Instance.skillName[1] = string.Empty;
                        child.GetComponent<EqumentBtn>()._thisSkill = null;
                    }
                    else if (child.name.Contains("4"))
                    {
                        PlayerSendInfo.Instance.skillName[2] = string.Empty;
                        child.GetComponent<EqumentBtn>()._thisSkill = null;
                    }
                }
            }
            
            
            if (EventSystem.current.currentSelectedGameObject.name.Contains("2"))
            {
                PlayerSendInfo.Instance.skillName[0] = _selectedSkill.name;
                EventSystem.current.currentSelectedGameObject.GetComponent<EqumentBtn>()._thisSkill = _selectedSkill;
            }
            else if(EventSystem.current.currentSelectedGameObject.name.Contains("3"))
            {
                PlayerSendInfo.Instance.skillName[1] = _selectedSkill.name;
                EventSystem.current.currentSelectedGameObject.GetComponent<EqumentBtn>()._thisSkill = _selectedSkill;
            }
            else if(EventSystem.current.currentSelectedGameObject.name.Contains("4"))
            {
                PlayerSendInfo.Instance.skillName[2] = _selectedSkill.name;
                EventSystem.current.currentSelectedGameObject.GetComponent<EqumentBtn>()._thisSkill = _selectedSkill;
            }
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            clickedImage.sprite = _selectedSkill.icon;
            if (clickedImage.sprite != null)
            {
                obj.transform.GetChild(0).gameObject.SetActive(true);
            }
            clickedImage = null;
            _selectedSkill = null;
        }
        

        private void HandleSkillSelect(SkillSelectEvent evt)
        {
            _selectedSkill = evt.selectedSkill;
        }
    }
}