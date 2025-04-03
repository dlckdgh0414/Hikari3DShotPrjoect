using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletSO currentBullet;

    private Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigid.linearVelocity = transform.forward * currentBullet.speed;
    }
}
