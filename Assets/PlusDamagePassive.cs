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
        Debug.Log("ππ¿”?");
    }
    private void OnDestroy()
    {
        _attackCompo.OnAttack -= PlusDamage;
    }

    private void PlusDamage()
    {
        if(CalculatePercent())
        {
            Debug.Log("¥Á√∑¿Ãø‰");
            DamagePassiveBullet projectile = PoolManager.Instance.Pop(bullet.name) as DamagePassiveBullet;
            projectile.transform.position = _player.transform.position;
            projectile.target = _aim.target.transform;
        }
    }

    private bool CalculatePercent()
    {
        Debug.Log("§ß§°§ß§°");
        int i = Random.Range(1,10);
        if (i <= 2) return true;
        else return false;
    }
}
