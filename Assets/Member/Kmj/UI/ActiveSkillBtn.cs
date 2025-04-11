using UnityEngine;
using UnityEngine.UI;

public class ActiveSkillBtn : MonoBehaviour
{
    [SerializeField] private SkillSO _skillSO;

    private Color thisImage;
    private SelectActiveBtn _saBtn;

    private void Awake()
    {
        _saBtn = GameObject.Find("InvenSkill").GetComponent<SelectActiveBtn>();
        thisImage = GetComponent<Image>().color;
        //thisImage = _skillSO.skillUIImage;
    }

    public void PressThieBtn()
    {
        if (_saBtn.currentListCount > 4)
            return;

        gameObject.GetComponent<Button>().interactable = false;
        _saBtn._invenList[_saBtn.currentListCount].TryGetComponent(out Image Image);

        Image.color = transform.GetComponent<Image>().color;
        _saBtn.currentListCount++;
    }
}
