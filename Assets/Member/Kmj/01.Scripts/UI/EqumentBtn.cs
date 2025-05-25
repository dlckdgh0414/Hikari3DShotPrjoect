using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EqumentBtn : MonoBehaviour
{
    private Button thisBtn;
    private Image thisImg;

    [SerializeField] private Sprite _baseImage;
    

    [field: SerializeField] public SkillSO _thisSkill { get; set; }
    private string path;
    
    
    private void Awake()
    {
        thisImg = GetComponent<Image>();    
        thisBtn = GetComponent<Button>();
        thisBtn.onClick.AddListener(ClickThis);
        
    }

    private void ClickThis()
    {
        if (thisImg.sprite != null)
        {
            if (_thisSkill != null  && PlayerSendInfo.Instance.skillName[0] == _thisSkill.name)
            {
                PlayerSendInfo.Instance.skillName[0] = string.Empty;
                _thisSkill = null;
            }

            else if (_thisSkill != null && PlayerSendInfo.Instance.skillName[1] == _thisSkill.name)
            {
                PlayerSendInfo.Instance.skillName[1] = string.Empty;
                _thisSkill = null;
            }
            else if (_thisSkill != null  && PlayerSendInfo.Instance.skillName[2] == _thisSkill.name)
            {
                PlayerSendInfo.Instance.skillName[2] = string.Empty;
                _thisSkill = null;
            }
            else
                return;
            
            thisImg.sprite = _baseImage;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
    
    
}
