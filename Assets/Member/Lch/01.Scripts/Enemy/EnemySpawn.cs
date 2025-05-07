using Member.Ysc._01_Code.UI;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private EnemySpawnListSO enemySpawnSO;
    private float _currentSpawnTime;
    
    [SerializeField] private GameProgressCheckUI gameProgressCheckUI;

    private void Update()
    {
        _currentSpawnTime += Time.deltaTime;
        if (_currentSpawnTime >= enemySpawnSO.SpawnTimer)
        {
            SpawnEnemy();
            _currentSpawnTime = 0;
        }
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < enemySpawnSO.SpawnCount; i++)
        {
            int randIndex = Random.Range(0, enemySpawnSO.enemies.Count);
            Enemy enemy = Instantiate(enemySpawnSO.enemies[randIndex], transform.position, Quaternion.identity);
            enemy.transform.SetParent(mainCamera.transform);
            enemy.OnRealDead.AddListener(gameProgressCheckUI.HandleEnemyDeadCount);
        }
    }
}
