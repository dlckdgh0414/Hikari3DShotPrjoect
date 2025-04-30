using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private SelectActiveBtn _saBtn;

    [SerializeField] private SkillInventorySO _inventorySO;
    [SerializeField] private UseSkillSO skillInventory;

    [SerializeField] private ThisType _type;
    private void Awake()
    {

        this.name = _skillSO.name;
        this.GetComponentInChildren<Image>().sprite = _skillSO.icon;
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


            skillInventory.UseSkillDictionary.Add(_skillSO.name, _skillSO);

            _saBtn._invenList[_saBtn.currentListCount].GetComponent<Image>().sprite = _skillSO.icon;

            _saBtn.currentListCount++;
        }
        else if(_type == ThisType.Static) 
        {
            _saBtn._invenList[0].GetComponent<Inven>().skillUI = gameObject;

            _saBtn._invenList[0].GetComponent<Inven>()._thisSkill = _skillSO;

            _saBtn._invenList[0].GetComponent<Image>().sprite = _skillSO.icon;


            skillInventory.UseSkillDictionary.Add(_skillSO.name, _skillSO);
        }
    }
}
