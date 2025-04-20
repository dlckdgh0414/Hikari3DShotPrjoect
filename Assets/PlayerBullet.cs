using Member.Ysc._01_Code.Combat.Bullet;
using UnityEngine;

public class PlayerBullet : BaseBullet, IPoolable
{
    public string ItemName => "PlayerBullet";
    private TrailRenderer line;

    protected override void Awake()
    {
        base.Awake();
        line = GetComponent<TrailRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        Debug.Log(other.gameObject.name);
        line.Clear();
        Hit();
        DestroyBullet(this);
    }
    private void OnValidate()
    {
        gameObject.name = ItemName;
    }
}
