using UnityEngine;

public class PlayerAttackCompo : MonoBehaviour,IEntityComponent
{
    private Player _player;

    private bool isAttack;

    private float fireRate = 1f;
    private float fireTimer = 0.8f;

    [SerializeField]
    private GameObject _bullet;

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


        Vector3 atkDir = (worldPosition - transform.position).normalized;
        Quaternion bulletRotation = Quaternion.LookRotation(atkDir);

        Instantiate(_bullet, transform.position, bulletRotation);
    }

    private void Debug(bool isClick)
    {
        isAttack = isClick;
    }
}
