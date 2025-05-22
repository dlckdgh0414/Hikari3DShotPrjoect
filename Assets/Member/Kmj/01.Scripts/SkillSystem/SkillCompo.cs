using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Member.Kmin._01_Script.Core.EventChannel;
using Member.Kmj._01.Scripts.Core.EventChannel;
using Member.Ysc._01_Code.StatSystems;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillCompo : MonoBehaviour, IEntityComponent
{
    public ActiveSkill firstSkill; 
    public ActiveSkill secondSkill;
    public ActiveSkill thirdSkill; 
    public ContactFilter2D whatIsEnemy;
    public Collider2D[] colliders;

    [SerializeField] private int maxCheckEnemy;

    public StatSO CoolDownStat;

    private Entity _entity;

    private Dictionary<Type, Skill> _skills;
    private List<PassiveSkill> _passiveSkills;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        colliders = new Collider2D[maxCheckEnemy];
        _skills = new Dictionary<Type, Skill>();
        _passiveSkills = new();
        GetComponentsInChildren<Skill>().Where(t => t.skillEnabled == true).ToList().ForEach(skill => _skills.Add(skill.GetType(), skill));
        GetComponentsInChildren<PassiveSkill>().Where(t => t.skillEnabled == true).ToList().ForEach(skill => _passiveSkills.Add(skill));
        _skills.Values.ToList().ForEach(skill => skill.InitializeSkill(_entity, this));
    }

    private void Update()
    {
        if (!_entity.IsGameStart) return;

        for (int i = 0; i < _passiveSkills.Count; i++)
        {
            if (_passiveSkills[i].IsPassiveCool) continue;
            _passiveSkills[i].PassiveAbility();
        }
    }

    public T GetSkill<T>() where T : Skill
    {
        Type type = typeof(T);
        return _skills.GetValueOrDefault(type) as T;
    }
}
