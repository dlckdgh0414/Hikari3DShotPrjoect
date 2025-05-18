using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.StatSystems;
using System;
using UnityEngine;


public delegate void CooldownInfo(float current, float totalTime);

public abstract class Skill : MonoBehaviour
{
    public bool skillEnabled = false;

    [SerializeField] protected float cooldown;

    protected float _cooldownTimer;
    protected Entity _entity;
    protected EntityMover _mover;
    protected Player _player;
    protected SkillCompo _skillCompo;
    protected EntityStat _statCompo;

    public bool IsCooldown => _cooldownTimer > 0f;
    public event CooldownInfo OnCooldown;
    public virtual void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        _entity = entity;
        _player = entity as Player;
        _skillCompo = skillCompo;
        _mover = entity.GetCompo<EntityMover>();
        _statCompo = entity.GetCompo<EntityStat>();
        _skillCompo.CoolDownStat = _statCompo.GetStat(_skillCompo.CoolDownStat);
    }

    protected virtual void Update()
    {
        if (_cooldownTimer > 0)
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
            _cooldownTimer = cooldown/_skillCompo.CoolDownStat.Value;
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
        //���⼭ ���߿� ��ų�� ������ �˷��ִ� �ǵ���� �ʿ��ϴ�.
    }

    public virtual void UseSkillWithoutCooltimeAndEffect()
    {
        //�ڵ��ߵ� ��ų���� �̿��ϱ� ���� ���� �Լ�.
    }
}