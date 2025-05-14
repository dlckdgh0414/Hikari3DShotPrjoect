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
        public SkillSO _staticSkill { get; set; }
        
        [SerializeField] private GameObject skillCompo;

        private string path;

        private void Awake()
        {
            _skillInvenEvent.AddListener<SkillSelectEvent>(HandleSkillSelect);
            _skillInvenEvent.AddListener<StaticSelectEvent>(HandleStaticSkillSelect);
            _inventorySO.invenSkillList.Add(tempSO1);
            _inventorySO.invenSkillList.Add(tempSO2);
            _inventorySO.invenSkillList.Add(tempSO3);
            
            path = AssetDatabase.GetAssetPath(skillCompo);
        }

        private void Start()
        {
            _staticButton.onClick.AddListener(HandleStaticSkillEquip);
            equipBtns.ForEach(btn => btn.onClick.AddListener(HandleSkillEquip));

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogError("Prefab을 찾을 수 없습니다: " + path);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            SkillCompo skillComponent = instance.GetComponentInChildren<SkillCompo>();
            if (skillComponent == null)
            {
                Debug.LogError("SkillCompo를 찾을 수 없습니다.");
                return;
            }

            skillComponent.secondSkill = null;
            skillComponent.firstSkill = null;
            skillComponent.thirdSkill = null;
            PrefabUtility.SaveAsPrefabAsset(instance, path);
            GameObject.DestroyImmediate(instance);
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
            
            
            if (EventSystem.current.currentSelectedGameObject.name.Contains("2"))
            {
                
                
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab == null)
                {
                    return;
                }
                
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                
                SkillCompo skillCompo = instance.GetComponentInChildren<SkillCompo>();
                if (skillCompo == null)
                {
                    return;
                }
                
                Transform child = skillCompo.transform.Find(_selectedSkill.name);
                if (child == null)
                {
                    return;
                }

                Skill skill = child.GetComponent<Skill>();
                if (skill == null)
                {
                    return;
                }
                
                skillCompo.firstSkill = skill;

                childTransform[0].GetComponent<EqumentBtn>()._thisSkill = _selectedSkill;
                
                PrefabUtility.SaveAsPrefabAsset(instance, path);
                GameObject.DestroyImmediate(instance);

                Debug.Log("Prefab이 성공적으로 수정되었습니다.");
            }
            else if(EventSystem.current.currentSelectedGameObject.name.Contains("3"))
            {

                // 👉 3. Prefab 로드
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab == null)
                {
                    Debug.LogError("Prefab을 찾을 수 없습니다: " + path);
                    return;
                }

                // 👉 4. Prefab 인스턴스를 임시로 생성 (Scene에 놓지 않고)
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                // 👉 5. SkillCompo 찾기
                SkillCompo skillCompo = instance.GetComponentInChildren<SkillCompo>();
                if (skillCompo == null)
                {
                    Debug.LogError("SkillCompo를 찾을 수 없습니다.");
                    return;
                }

                // 👉 6. 자식 중 Skill 이름에 해당하는 Transform 찾기
                Transform child = skillCompo.transform.Find(_selectedSkill.name);
                if (child == null)
                {
                    Debug.LogError($"자식 오브젝트 '{_selectedSkill.name}'를 찾을 수 없습니다.");
                    return;
                }

                Skill skill = child.GetComponent<Skill>();
                if (skill == null)
                {
                    Debug.LogError("Skill 컴포넌트를 찾을 수 없습니다.");
                    return;
                }

                // 👉 7. SkillCompo에 Skill 할당
                skillCompo.secondSkill = skill;
                childTransform[1].GetComponent<EqumentBtn>()._thisSkill = _selectedSkill;

                // 👉 8. Prefab에 저장
                PrefabUtility.SaveAsPrefabAsset(instance, path);
                GameObject.DestroyImmediate(instance);

                Debug.Log("Prefab이 성공적으로 수정되었습니다.");
            }
            else if(EventSystem.current.currentSelectedGameObject.name.Contains("4"))
            {

                // 👉 3. Prefab 로드
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab == null)
                {
                    Debug.LogError("Prefab을 찾을 수 없습니다: " + path);
                    return;
                }

                // 👉 4. Prefab 인스턴스를 임시로 생성 (Scene에 놓지 않고)
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                // 👉 5. SkillCompo 찾기
                SkillCompo skillCompo = instance.GetComponentInChildren<SkillCompo>();
                if (skillCompo == null)
                {
                    Debug.LogError("SkillCompo를 찾을 수 없습니다.");
                    return;
                }

                // 👉 6. 자식 중 Skill 이름에 해당하는 Transform 찾기
                Transform child = skillCompo.transform.Find(_selectedSkill.name);
                if (child == null)
                {
                    Debug.LogError($"자식 오브젝트 '{_selectedSkill.name}'를 찾을 수 없습니다.");
                    return;
                }

                Skill skill = child.GetComponent<Skill>();
                if (skill == null)
                {
                    Debug.LogError("Skill 컴포넌트를 찾을 수 없습니다.");
                    return;
                }

                // 👉 7. SkillCompo에 Skill 할당
                skillCompo.thirdSkill = skill;

                childTransform[2].GetComponent<EqumentBtn>()._thisSkill = _selectedSkill;
                // 👉 8. Prefab에 저장
                PrefabUtility.SaveAsPrefabAsset(instance, path);
                GameObject.DestroyImmediate(instance);

                Debug.Log("Prefab이 성공적으로 수정되었습니다.");
            }
            
            clickedImage.sprite = _selectedSkill.icon;
            
            clickedImage = null;
            _selectedSkill = null;
            
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
            //_staticSkilEvent.staticSkill = _staticSkill.name;
            //_skillSendEvent.RaiseEvent(_skillEvent);
            
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