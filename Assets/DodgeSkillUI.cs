using UnityEngine;

public class DodgeSkillUI : SkillCoolDownUI
{
    private Skill _currentSkill;
    private float _cooltime;

    private void Start()
    {
        _currentSkill = _skillCompo.GetSkill<DodgeSkill>();
        _currentSkill.OnCooldown += CooldownInfo;
    }

    protected override void CooldownInfo(float current, float totalTime)//5
    {
        base.CooldownInfo(current,totalTime);
        bool isAtv = current < 0.1f ? false : true;
        text.gameObject.SetActive(isAtv);
        text.text = (current + 1).ToString().Substring(0,1);
        _cooltime = totalTime;
        _iconCool.fillAmount = current / _cooltime;
    }
}
