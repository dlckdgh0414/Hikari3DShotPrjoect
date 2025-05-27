using Ami.BroAudio;
using UnityEngine;

public class PlusDamagePassive : PassiveSkill
{
    [SerializeField] private DamagePassiveBullet bullet;
    private PlayerAttackCompo _attackCompo;
    private AutoAimCompo _aim;

    public override void InitializeSkill(Entity entity, SkillCompo skillCompo)
    {
        base.InitializeSkill(entity, skillCompo);
        _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        _attackCompo.OnAttack += PlusDamage;
        _aim = entity.GetCompo<AutoAimCompo>();
        Debug.Log("����?");
    }
    private void OnDestroy()
    {
        if(skillEnabled)
        _attackCompo.OnAttack -= PlusDamage;
    }

    private void PlusDamage()
    {
        if(CalculatePercent())
        {
            Debug.Log("��÷�̿�");
            DamagePassiveBullet projectile = PoolManager.Instance.Pop(bullet.name) as DamagePassiveBullet;
            projectile.transform.position = _player.transform.position;
            BroAudio.Play(skillSound);
            projectile.target = _aim.target.transform;
        }
    }

    private bool CalculatePercent()
    {
        if (_aim.target == false) return false;
        int i = Random.Range(1,10);
        if (i <= 2) return true;
        else return false;
    }
}
