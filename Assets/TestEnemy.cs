using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private void OnEnable()
    {
        EnemyManager.Register(gameObject);
    }

    private void OnDisable()
    {
        EnemyManager.Unregister(gameObject);
    }
}
