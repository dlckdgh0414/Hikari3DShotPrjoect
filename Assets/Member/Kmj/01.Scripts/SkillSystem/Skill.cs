using Member.Ysc._01_Code.Agent;
using Member.Ysc._01_Code.StatSystems;
using System;
using UnityEngine;



public abstract class Skill : MonoBehaviour
{
    protected EntityVFX entityVFX;

    public bool skillEnabled = false;

   
    protected Entity _entity;
    protected EntityMover _mover;
    protected Player _player;
    protected SkillCompo _skillCompo;
    protected EntityStat _statCompo;

    public Sprite skillIcon;
    
    public virtual void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        _entity = entity;
        _player = entity as Player;
        _skillCompo = skillCompo;
        _mover = entity.GetCompo<EntityMover>();
        _statCompo = entity.GetCompo<EntityStat>();
        _skillCompo.CoolDownStat = _statCompo.GetStat(_skillCompo.CoolDownStat);
        entityVFX = _entity.GetCompo<EntityVFX>();
    }

    
}