using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected BulletSettingSO BulletSO;
    public abstract void EnemyAttack(Transform target);

}
