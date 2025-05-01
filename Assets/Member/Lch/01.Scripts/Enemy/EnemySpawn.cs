using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private EnemySpawnListSO enemySpawnSO;
    [SerializeField] private float spawnDistance = 10f;

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = GetSpawnPositionOutsideCamera();
        for(int i = 0; i < enemySpawnSO.SpawnCount; i++)
        {
            int randIndex = Random.Range(0, enemySpawnSO.enemies.Count);
            Enemy enemy = Instantiate(enemySpawnSO.enemies[randIndex], transform.position, Quaternion.identity);
            enemy.transform.SetParent(mainCamera.transform);
        }
    }

    private Vector3 GetSpawnPositionOutsideCamera()
    {
        Vector3 viewportPos = new Vector3(Random.Range(-0.2f, 1.2f), Random.Range(-0.2f, 1.2f), spawnDistance);
        return viewportPos;
    }
}
