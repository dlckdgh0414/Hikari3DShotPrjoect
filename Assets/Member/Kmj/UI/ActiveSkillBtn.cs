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
        if (_saBtn.currentListCount >= _saBtn._invenList.Count)
            return;

        if (_type == ThisType.Static)
            if (_saBtn.StaticBtn != null)
                return;

        gameObject.GetComponent<Button>().interactable = false;

        if (_type == ThisType.Normal)
        {
            _saBtn._invenList[_saBtn.currentListCount].TryGetComponent(out Inven iven);

            iven.skillUI = gameObject;

            iven._thisSkill = _skillSO;

            _saBtn._invenList[_saBtn.currentListCount].GetComponent<Image>().sprite = _skillSO.skillUIImage;

            _saBtn.currentListCount++;
        }
        else if(_type == ThisType.Static) 
        {
            _saBtn._invenList[0].GetComponent<Inven>().skillUI = gameObject;

            _saBtn._invenList[0].GetComponent<Inven>()._thisSkill = _skillSO;

            _saBtn._invenList[0].GetComponent<Image>().sprite = _skillSO.skillUIImage;
        }
    }
}
