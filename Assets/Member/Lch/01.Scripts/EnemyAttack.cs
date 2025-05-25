using DG.Tweening;
using Member.Ysc._01_Code.Combats;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    private bool _canDamage = true;
    public Entity _entity;

    private void OnTriggerStay(Collider other)
    {
        if (!_canDamage) return;

        if (other.TryGetComponent(out Player player))
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(damage);
                _canDamage = false;

                // ÄðÅ¸ÀÓ ½ÃÀÛ
                DOVirtual.DelayedCall(1.5f, () => _canDamage = true);
            }
        }
    }
}
