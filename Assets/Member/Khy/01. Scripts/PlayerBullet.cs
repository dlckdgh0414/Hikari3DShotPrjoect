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
    protected override void FixedUpdate()
    {
        RbCompo.linearVelocity = fireDirection.normalized * BulletSO.BulletSpeed;
        Quaternion quaternion = Quaternion.LookRotation(fireDirection);
        transform.rotation = Quaternion.Euler(-90, 0, quaternion.z);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;
        Hit(other);
        DestroyBullet(this);
        line.Clear();
    }
}
