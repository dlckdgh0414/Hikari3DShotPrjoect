using Member.Kmin._01_Script.Core.EventChannel;
using UnityEngine;
using UnityEngine.UI;

namespace Member.Kmj._01.Scripts.UI.Inventory
{
    public class StaticSkillBtn : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _skillEventChannel;
        [field : SerializeField] public SkillSO thisSkill { get; set; }

        private StaticSelectEvent _skillEvent = SkillEquipEventChannel.staticSkillEquipEvent;
    
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
            _skillEvent.staticSkill = thisSkill.ThisSkill;
            _skillEventChannel.RaiseEvent(_skillEvent);
        }
    }
}