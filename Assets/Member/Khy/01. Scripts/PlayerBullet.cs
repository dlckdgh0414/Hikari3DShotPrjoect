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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        line.Clear();
    }

}
