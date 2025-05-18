using Member.Ysc._01_Code.Combat.Bullet;
using UnityEngine;

public class PlayerBullet : BaseBullet, IPoolable
{
    private TrailRenderer line;

    protected override void Awake()
    {
        base.Awake();
        line = GetComponent<TrailRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;
        Hit(other);
        DestroyBullet(this);
        line.Clear();
    }
}
