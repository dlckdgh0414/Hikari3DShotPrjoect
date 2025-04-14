using Member.Ysc._01_Code.Combat.Bullet;
using UnityEngine;

public class PlayerAttackCompo : MonoBehaviour,IEntityComponent
{
    private Player _player;

    private bool isAttack;

    [SerializeField]
    private GameObject muzzle;

    [SerializeField]
    private float fireRate = 1f;
    private float fireTimer = 0.8f;

    [SerializeField]
    private BaseBullet _bullet;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _player.InputReader.OnAttackEvent += Debug;
    }

    private void OnDestroy()
    {
        _player.InputReader.OnAttackEvent -= Debug;
    }

    private void Update()
    {
        if(isAttack)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= 1f / fireRate)
            {
                fireTimer = 0f;
                FireBullet();
            }
        }
    }

    private void FireBullet()
    {
        Vector3 worldPosition = _player.InputReader.GetWorldPosition(out RaycastHit hitInfo);
        BaseBullet bullet = PoolManager.Instance.Pop(_bullet.name) as BaseBullet;
        bullet.transform.position = muzzle.transform.position;
        bullet.SetDirection(worldPosition);
    }

    private void Debug(bool isClick)
    {
        isAttack = isClick;
    }
}
