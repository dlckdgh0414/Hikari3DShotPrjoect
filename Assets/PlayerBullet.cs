using Member.Ysc._01_Code.Combat.Bullet;
using UnityEngine;

public class PlayerBullet : BaseBullet, IPoolable
{
    public string ItemName => "PlayerBullet";

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        DestroyBullet();
    }
    private void OnValidate()
    {
        gameObject.name = ItemName;
    }
    protected override void DestroyBullet()
    {
        PoolManager.Instance.Push(this);
    }
}
