using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDownUI : MonoBehaviour,IEntityComponent
{
    protected Image _iconCool;
    protected SkillCompo _skillCompo;

    public void Initialize(Entity entity)
    {
        _iconCool = transform.GetChild(0).GetComponent<Image>();

        _skillCompo = entity.GetCompo<SkillCompo>();
    }

    protected virtual void CooldownInfo(float current, float totalTime)
    {

    }
}
