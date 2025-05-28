using System;
using System.Collections.Generic;
using System.Linq;
using Member.Kmin._01_Script.SkillTree;
using Member.Ysc._01_Code.StatSystems;
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
    private Dictionary<string,ActiveSkill> _canSelectSkills = new();

    public void Initialize(Entity entity)
    {
        _entity = entity;
        colliders = new Collider2D[maxCheckEnemy];
        _skills = new Dictionary<Type, Skill>();
        _passiveSkills = new();
        SaveNodeStat.Instance.LoadStat();
        GetComponentsInChildren<Skill>().Where(t => t.skillEnabled == true).ToList().ForEach(skill => _skills.Add(skill.GetType(), skill));
        GetComponentsInChildren<PassiveSkill>().Where(t => t.skillEnabled == true).ToList().ForEach(skill => _passiveSkills.Add(skill));
        GetComponentsInChildren<ActiveSkill>().Where(t => t.skillEnabled == true).ToList().ForEach(skill => _canSelectSkills.Add(skill.gameObject.name, skill));
        
        _skills.Values.ToList().ForEach(skill => skill.InitializeSkill(_entity, this));

        if(!PlayerSendInfo.Instance.DontSelectAllSkills())
        {
            firstSkill = _canSelectSkills[PlayerSendInfo.Instance.skillName[0]];
            secondSkill = _canSelectSkills[PlayerSendInfo.Instance.skillName[1]];
            thirdSkill = _canSelectSkills[PlayerSendInfo.Instance.skillName[2]];
        }
    }

    private void Update()
    {
        if (!Entity.IsGameStart) return;

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
