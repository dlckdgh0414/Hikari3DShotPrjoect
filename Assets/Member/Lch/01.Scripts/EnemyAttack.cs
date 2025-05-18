using Member.Ysc._01_Code.Combats;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    public Entity _entity;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            IDamageable damageable = player.GetComponentInChildren<IDamageable>();
            damageable.ApplyDamage(damage);
        }
    }
}
