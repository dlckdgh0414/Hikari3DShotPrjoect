using UnityEngine;
using UnityEngine.UI;

public class ActiveSkillBtn : MonoBehaviour
{
    [SerializeField] private SkillSO _skillSO;

    private Sprite thisImage;
    private SelectActiveBtn _saBtn;
    private bool _isAct;

    private void Awake()
    {
        thisImage = GetComponent<Sprite>();
        thisImage = _skillSO.skillUIImage;
    }

    public void PressThieBtn()
    {
        if (_saBtn.currentListCount > 4)
            return;

        if(_isAct)
        {
            _isAct = false;
        }
        _saBtn._invenList[_saBtn.currentListCount].TryGetComponent(out Sprite Image);

        Image = this.thisImage;
        _saBtn.currentListCount++;
        _isAct = true;
    }
}
