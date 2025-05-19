using UnityEngine;

public class FirstSkillUI : SkillCoolDownUI
{
    private float _cooltime;

    protected override void InitializeCooldownUI()
    {
        base.InitializeCooldownUI();
        _currentSkill = _skillCompo.firstSkill;
        _currentSkill.OnCooldown += CooldownInfo;
        _iconImage.sprite = _currentSkill.skillIcon;
    }
    protected override void CooldownInfo(float current, float totalTime)//5
    {
        base.CooldownInfo(current, totalTime);
    }
}
