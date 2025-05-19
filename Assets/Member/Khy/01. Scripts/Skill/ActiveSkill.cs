using UnityEngine;

public delegate void CooldownInfo(float current, float totalTime);

public class ActiveSkill : Skill
{
    [SerializeField] protected float cooldown;

    protected float _cooldownTimer;
    public bool IsCooldown => _cooldownTimer > 0f;
    public event CooldownInfo OnCooldown;

    protected virtual void Update()
    {
        if (IsCooldown)
        {
            _cooldownTimer -= Time.deltaTime;

            if (_cooldownTimer <= 0)
                OverSkillCooltime();
            OnCooldown?.Invoke(_cooldownTimer, cooldown);
        }
    }

    public virtual bool AttemptUseSkill()
    {
        if (_cooldownTimer <= 0 && skillEnabled)
        {
            _cooldownTimer = cooldown / _skillCompo.CoolDownStat.Value;
            UseSkill();
            return true;
        }
        Debug.Log("Skill cooldown or locked");
        return false;
    }

    public virtual void OverSkillCooltime()
    {
        _cooldownTimer = 0;
        Debug.Log("Skill enable");
    }


    public virtual void UseSkill()
    {
    }

    public virtual void UseSkillWithoutCooltimeAndEffect()
    {
    }
}
