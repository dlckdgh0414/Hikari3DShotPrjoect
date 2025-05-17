using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCoolDownUI : MonoBehaviour,IEntityComponent
{
    protected Image _iconCool;
    protected SkillCompo _skillCompo;

    public TextMeshProUGUI text;

    private bool isEndCool;

    public void Initialize(Entity entity)
    {
        _iconCool = transform.GetChild(0).GetComponent<Image>();
        text.gameObject.SetActive(false);
        _skillCompo = entity.GetCompo<SkillCompo>();
    }

    protected virtual void CooldownInfo(float current, float totalTime)
    {
    }
}
