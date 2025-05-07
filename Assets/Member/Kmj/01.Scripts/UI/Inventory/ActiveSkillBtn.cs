using Member.Kmin._01_Script.Core.EventChannel;
using UnityEngine;
using UnityEngine.UI;


public class ActiveSkillBtn : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO _skillEventChannel;
    [field : SerializeField] public SkillSO thisSkill { get; set; }
    
    private SkillSelectEvent _skillEvent = SkillEquipEventChannel.SkillSelectEvent;
    
    private Button thisBtn;
    private Image thisImg;

    private void Awake()
    {
        thisBtn = GetComponent<Button>();
        thisImg = thisBtn.GetComponent<Image>();
        thisImg.sprite = thisSkill.icon;
        thisBtn.onClick.AddListener(ClickThis);
    }

    private void ClickThis()
    {
        _skillEvent.selectedSkill = thisSkill.ThisSkill;
        _skillEventChannel.RaiseEvent(_skillEvent);
    }
}
