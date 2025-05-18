using UnityEngine;

public class ThirdSkillUI : SkillCoolDownUI
{
    private Skill _currentSkill;
    private float _cooltime;

    private void Start()
    {
        _currentSkill = _skillCompo.thirdSkill;
        _currentSkill.OnCooldown += CooldownInfo;
    }

    protected override void CooldownInfo(float current, float totalTime)//5
    {
        base.CooldownInfo(current, totalTime);
        Debug.Log($"{current}");
        bool isAtv = current < 0.1f ? false : true;
        text.gameObject.SetActive(isAtv);
        text.text = (current + 1).ToString().Substring(0, 1);
        _cooltime = totalTime;
        _iconCool.fillAmount = current / _cooltime;
    }
}
