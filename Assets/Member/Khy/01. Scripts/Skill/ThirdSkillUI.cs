using UnityEngine;

public class ThirdSkillUI : SkillCoolDownUI
{
    protected override void InitializeCooldownUI()
    {
        base.InitializeCooldownUI();
        _currentSkill = _skillCompo.thirdSkill;
        _currentSkill.OnCooldown += CooldownInfo;
        _iconImage.sprite = _currentSkill.skillIcon;
    }
    protected override void CooldownInfo(float current, float totalTime)//5
    {
        base.CooldownInfo(current, totalTime);
    }
}
