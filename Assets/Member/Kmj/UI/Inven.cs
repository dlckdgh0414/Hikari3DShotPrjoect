
using UnityEngine;
using UnityEngine.UI;
enum ButtonType
{
    normal,
    Static,
}
public class Inven : MonoBehaviour
{
    [SerializeField] private int _invenNum;
    private Image _image;
    [SerializeField] private SelectActiveBtn _selctManager;
    [SerializeField] private GameObject Parent;
    [SerializeField] private ButtonType type;

    [field: SerializeField] public SkillSO _thisSkill;

    [field: SerializeField] public GameObject skillUI;
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
        if (skillUI == null)
            return;
        
        if (type == ButtonType.normal && _selctManager._invenList.Contains(gameObject))
        {
            skillUI.GetComponentInChildren<Button>().interactable = true;
            _selctManager._invenList.Remove(gameObject);
            _image = null;
            _selctManager._invenList.Add(gameObject);


            transform.SetParent(Parent.transform);
            transform.SetAsLastSibling();

            _selctManager.UseSkillDictionary.Remove(_thisSkill.skillName);
            _thisSkill = null;
            skillUI = null;
            transform.GetComponentInChildren<Image>().sprite = null;
            _selctManager.currentListCount--;
        }

        if (type == ButtonType.Static && _selctManager._invenList.Contains(gameObject))
        {
            _image = null;

            _selctManager.UseSkillDictionary.Remove(_thisSkill.skillName);

            skillUI.GetComponentInChildren<Button>().interactable = true;
            transform.GetComponentInChildren<Image>().sprite = null;
            skillUI = null;

            _thisSkill = null;
        }

    }
}
