using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDistance = 10f;

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = GetSpawnPositionOutsideCamera();
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    private Vector3 GetSpawnPositionOutsideCamera()
    {
        Vector3 viewportPos = new Vector3(Random.Range(-0.2f, 1.2f), Random.Range(-0.2f, 1.2f), spawnDistance);
        return viewportPos;
    }
}
