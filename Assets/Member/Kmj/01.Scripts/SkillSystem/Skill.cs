using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Data;


public delegate void CooldownInfo(float current, float totalTime);
public abstract class Skill : ScriptableObject
{
 /*   public bool SkillEnable = false;

    [SerializeField] private float cooldown;
    protected float _cooldownTimer;
    protected Entity _entity;
    protected SkillCompo _skillCOmpo;

    public bool IsCooldown => _cooldownTimer > 0f;
    public event CooldownInfo OnCooldown;

    public virtual void Init(Entity entity,SkillCompo skillCompo)
    {
        _entity = entity;
        _skillCOmpo = skillCompo;
    }

    protected virtual void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;

            if (_cooldownTimer <= 0)
                _cooldownTimer = 0;

            OnCooldown?.Invoke(_cooldownTimer, cooldown);
        }
    }
    public virtual void UseSkill()
    {
        //���⼭ ���߿� ��ų�� ������ �˷��ִ� �ǵ���� �ʿ��ϴ�.
    }

    public virtual void UseSkillWithoutCooltimeAndEffect()
    {
        //�ڵ��ߵ� ��ų���� �̿��ϱ� ���� ���� �Լ�
    }
*/
}
