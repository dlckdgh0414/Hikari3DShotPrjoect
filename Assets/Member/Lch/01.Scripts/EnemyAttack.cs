using Member.Ysc._01_Code.Combats;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    public Entity _entity;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log("데미지받아라");
                damageable.ApplyDamage(damage);
            }
        }
    }
}
