using UnityEngine;
using UnityEngine.InputSystem;

public class FirstSkillUI : SkillCoolDownUI
{
    protected override void InitializeCooldownUI()
    {
        base.InitializeCooldownUI();
        _currentSkill = _skillCompo.firstSkill;
        _currentSkill.OnCooldown += CooldownInfo;
        _iconImage.sprite = _currentSkill.skillIcon;

        var binding = _skillInputKey.action.bindings[0];
        string path = binding.effectivePath;
        string displayName = InputControlPath.ToHumanReadableString(
            path,
            InputControlPath.HumanReadableStringOptions.OmitDevice
        );

        whatKeyText.text = displayName;
    }
    protected override void CooldownInfo(float current, float totalTime)//5
    {
        base.CooldownInfo(current, totalTime);
    }
}
