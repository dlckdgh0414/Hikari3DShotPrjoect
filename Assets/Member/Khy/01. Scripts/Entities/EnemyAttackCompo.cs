using Member.Ysc._01_Code.Combat.Bullet;
using UnityEngine;

public class EnemyAttackCompo : AttackCompo, IEntityComponent
{
    [SerializeField]
    private BaseBullet _bullet;

    public void Initialize(Entity entity)
    {
        _bullet._attackCompo = this;
    }
}
