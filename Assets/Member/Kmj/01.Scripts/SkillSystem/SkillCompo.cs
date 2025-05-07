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

    private GameEventChannelSO _eventChannel;


    public void Initialize(Entity entity)
    {
        _entity = entity;
        colliders = new Collider2D[maxCheckEnemy];
        entityVFX = entity.GetCompo<EntityVFX>();
        _skills = new Dictionary<Type, Skill>();
        GetComponentsInChildren<Skill>().ToList().ForEach(skill => _skills.Add(skill.GetType(), skill));
        _skills.Values.ToList().ForEach(skill => skill.InitializeSkill(_entity, this));
        
        _eventChannel.AddListener<SendSkill>(HandleSendSkill);
        _eventChannel.AddListener<SendStaticSkill>(HandleStaticSkill);
    }

    private void HandleSendSkill(SendSkill evt)
    {
        Transform child = transform.Find(evt.selectedSkill);
        
        if (child != null)
        {
            Skill skill = child.GetComponent<Skill>();
            

            if (secondSkill == null)
            {
                secondSkill = skill;
            }
            else if(thirdSkill == null)
            {
                thirdSkill = skill;
            }
            
        }
    }
        
    private void HandleStaticSkill(SendStaticSkill evt)
    {
        Transform child = transform.Find(evt.staticSkill);

        if (child != null)
        {
            Skill skill = child.GetComponent<Skill>();
            
            firstSkill = skill;
        }
    }
    
    
    public T GetSkill<T>() where T : Skill
    {
        Type type = typeof(T);
        return _skills.GetValueOrDefault(type) as T;
    }
}
