using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EqumentBtn : MonoBehaviour
{
    private Button thisBtn;
    private Image thisImg;
    
    [SerializeField] private GameObject skillCompo;

    [field: SerializeField] public SkillSO _thisSkill { get; set; }
    private string path;
    
    
    private void Awake()
    {
        thisImg = GetComponent<Image>();    
        thisBtn = GetComponent<Button>();
        thisBtn.onClick.AddListener(ClickThis);
        
        path = AssetDatabase.GetAssetPath(skillCompo);
    }

    private void ClickThis()
    {
        if (thisImg.sprite != null)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    
            print(prefab);
                    
            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    
            print(instance);
                
            SkillCompo skillCompo = instance.GetComponentInChildren<SkillCompo>();
                    
            print(skillCompo);
                
            Transform baby = skillCompo.transform.Find(_thisSkill.name); 
                    
            print(baby);
                    

            Skill skill = baby.GetComponent<Skill>();
                    
            print(skill.name);

            if (skillCompo.firstSkill == skill)
            {
                skillCompo.firstSkill = null;
                _thisSkill = null;
            }

            else if (skillCompo.secondSkill == skill)
            {
                skillCompo.secondSkill = null;
                _thisSkill = null;
            }
            else if (skillCompo.thirdSkill == skill)
            {
                skillCompo.thirdSkill = null;
                _thisSkill = null;
            }
            else
                return;
                        
            PrefabUtility.SaveAsPrefabAsset(instance, path);
            GameObject.DestroyImmediate(instance);
            
            thisImg.sprite = null;
        }
        
    }
    
    
}
