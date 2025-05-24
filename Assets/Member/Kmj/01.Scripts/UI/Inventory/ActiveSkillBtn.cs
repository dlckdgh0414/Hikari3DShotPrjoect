using Member.Kmin._01_Script.Core.EventChannel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ActiveSkillBtn : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private GameEventChannelSO _skillEventChannel;
    [field : SerializeField] public SkillSO thisSkill { get; set; }
    
    private SkillSelectEvent _skillEvent = SkillEquipEventChannel.SkillSelectEvent;
    
    private Button thisBtn;
    private Image thisImg;

    public bool isDontSelect;

    private void Awake()
    {
        thisBtn = GetComponent<Button>();
        thisImg = thisBtn.GetComponent<Image>();
        thisImg.sprite = thisSkill.icon;
        thisBtn.onClick.AddListener(ClickThis);
    }

    private void ClickThis()
    {
        if (isDontSelect) return;
        _skillEvent.selectedSkill = thisSkill;
        _skillEventChannel.RaiseEvent(_skillEvent);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindAnyObjectByType<SelectKingdomsLogic>().Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindAnyObjectByType<SelectKingdomsLogic>().Show(thisSkill.description);
    }
}
