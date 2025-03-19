using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*public enum SkillKeyEnum
{
    FirstSkill,
    SecondSkill,
    ThirdSkill
}

public class SkillCompo : MonoBehaviour, IEntiyComponent
{

    private Dictionary<Skill, SkillKeyEnum> _activeSkill;


    private Entity _entity;

    private Dictionary<Type, Skill> _skills;

    public void Init(Entity entity)
    {  
        _entity= entity;

        _skills = new Dictionary<Type, Skill>();
        _activeSkill = new Dictionary<Skill, SkillKeyEnum>();
        GetComponentsInChildren<Skill>().ToList().ForEach(skill => _skills.Add(skill.GetType(), skill));
        _skills.Values.ToList().ForEach(skill => skill.Init(entity,this));
    }

    public T GetSkill<T>() where T : Skill
    {
        Type type = typeof(T);
        return _skills.GetValueOrDefault(type) as T;
    }

    public void AddActiveSkill(Skill skill, SkillKeyEnum skillKey)
    {
        skill.SkillEnable = true;
        _activeSkill.Add(skill, skillKey);
    }

    public void RemoveActiveSkill(Skill skill)
    {
        skill.SkillEnable = false;
        _activeSkill.Remove(skill);
    }
}*/
