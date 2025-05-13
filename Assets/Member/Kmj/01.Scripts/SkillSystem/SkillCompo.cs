using System;
using System.Collections.Generic;
using System.Linq;
using Member.Kmin._01_Script.Core.EventChannel;
using Member.Kmj._01.Scripts.Core.EventChannel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillCompo : MonoBehaviour, IEntityComponent
{

    public Skill firstSkill; 
    public Skill secondSkill;
    public Skill thirdSkill; 
    public ContactFilter2D whatIsEnemy;
    public Collider2D[] colliders;
    [HideInInspector] public EntityVFX entityVFX;

    [SerializeField] private int maxCheckEnemy;

    private Entity _entity;

    private Dictionary<Type, Skill> _skills;


    public void Initialize(Entity entity)
    {
        _entity = entity;
        colliders = new Collider2D[maxCheckEnemy];
        entityVFX = entity.GetCompo<EntityVFX>();
        _skills = new Dictionary<Type, Skill>();
        GetComponentsInChildren<Skill>().ToList().ForEach(skill => _skills.Add(skill.GetType(), skill));
        _skills.Values.ToList().ForEach(skill => skill.InitializeSkill(_entity, this));
        
    }
    
    
    
    public T GetSkill<T>() where T : Skill
    {
        Type type = typeof(T);
        return _skills.GetValueOrDefault(type) as T;
    }
}
