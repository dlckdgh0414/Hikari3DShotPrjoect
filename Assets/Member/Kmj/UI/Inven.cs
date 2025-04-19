using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inven : MonoBehaviour
{
    [SerializeField] private int _invenNum;
    private Image _image;
    [SerializeField] private SelectActiveBtn _selctManager;
    [SerializeField] private GameObject Parent;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _invenNum = _selctManager._invenList.IndexOf(gameObject);
    }
    public void ReMoveThis()
    {
        if (_image.color == Color.white)
            return;
        
        if (_selctManager._invenList.Contains(gameObject))
        {
            _selctManager._invenList.Remove(gameObject);
            _selctManager._invenList.Add(gameObject);
            GameObject SkillUI = GameObject.Find(_selctManager._thisSkill.skillName);
            SkillUI.GetComponent<Button>().interactable = true;
            _selctManager._thisSkill = null;
        }

       
        transform.SetParent(Parent.transform); 
        transform.SetAsLastSibling();

        _selctManager.currentListCount--;

    }
}
