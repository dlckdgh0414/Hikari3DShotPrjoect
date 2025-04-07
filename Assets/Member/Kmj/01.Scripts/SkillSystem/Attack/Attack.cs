using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Attack", menuName = "SO/Skill/AttackSkill")]
/*public class Attack : Skill , IAfterInit
{

    [SerializeField] private OverlapDamgeCaster damageCaster;
    [SerializeField] protected AttackDataSO attackData;

    [SerializeField] private AttackDataListSO attackDataList;

    private Player _player;
    private EntityMover _mover;
    private EntityAnimationTrigger _triggerCompo;

    private Dictionary<string, AttackDataSO> _attackDataDictionary;
    private AttackDataSO _currentAttackData;

    public override void Init(Entity entity, SkillCompo skillCompo)
    {
        base.Init(entity, skillCompo);
        _player = entity as Player;
        _renderer = entity.GetCompo<EntityRenderer>();
        _mover = entity.GetCompo<EntityMover>();
        _triggerCompo = entity.GetCompo<EntityAnimationTrigger>();
        damageCaster.InitInPlayerCaster(entity);

        _attackDataDictionary = new Dictionary<string, AttackDataSO>();
        attackDataList.attackDatas.ForEach(attackData => _attackDataDictionary.Add(attackData.attackName, attackData));
    }
    public void AfterInit()
    {
        _triggerCompo.OnAttackTrigger += HandleMeleeAttackTrigger;
    }

    private void OnDestroy()
    {
        _triggerCompo.OnAttackTrigger -= HandleMeleeAttackTrigger;
    }

    public virtual AttackDataSO CurrentAttackData => _currentAttackData = attackData;

    public virtual AttackDataSO GetAttackData(string attackName)
    {
        AttackDataSO data = _attackDataDictionary.GetValueOrDefault(attackName);
        Debug.Assert(data != null, $"request attack data is not exist : {attackName}");
        return data;
    }

    public virtual void SetAttackData(AttackDataSO attackData)
    {
        _currentAttackData = attackData;
    }

    protected virtual void HandleMeleeAttackTrigger()
    {
        float damage = 5f;
        Vector2 knockBackForce = _currentAttackData.knockBackForce;
        bool success = damageCaster.CastDamage(damage, knockBackForce, _currentAttackData.isPowerAttack);

        if (success)
        {
            _player.PlayerChannel.RaiseEvent(PlayerEvents.PlayerAttackSuccess);
        }
    }

    protected virtual void HandleRangedAttack()
    {
        //원거리 공격할때 알아서 재정의하고 구현하기
    }
}*/
