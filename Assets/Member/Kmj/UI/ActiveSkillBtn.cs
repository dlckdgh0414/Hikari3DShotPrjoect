using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ThisType
{
    Normal,
    Static,
}
public class ActiveSkillBtn : MonoBehaviour
{
    [SerializeField] private SkillSO _skillSO;

    private Color thisImage;
    private SelectActiveBtn _saBtn;

    [SerializeField] private SkillInventorySO _inventorySO;

    [SerializeField] private ThisType _type;
    private void Awake()
    {
        _saBtn = GameObject.Find("InvenSkill").GetComponent<SelectActiveBtn>();

        this.name = _skillSO.name;  
    }

    public void PressThieBtn()
    {
        if (_saBtn.currentListCount > _saBtn._invenList.Count)
            return;

        if (_type == ThisType.Static)
            if (_saBtn.StaticBtn != null)
                return;

        gameObject.GetComponent<Button>().interactable = false;


        if (_type == ThisType.Normal)
        {
            SkillSO skill = _inventorySO.normalSkillList.GetValueOrDefault(_skillSO.skillName);
            _saBtn._thisSkill = skill;
            _saBtn.transform.TryGetComponent(out Image image);
            image.sprite = skill.skillUIImage;
        }
        else
        {
            SkillSO skill = _inventorySO.staticSkillList.GetValueOrDefault(_skillSO.skillName);
            _saBtn._thisSkill = skill;
            _saBtn.transform.TryGetComponent(out Image image);
            image.sprite = skill.skillUIImage;
        }



        _saBtn.currentListCount++;
    }
}
