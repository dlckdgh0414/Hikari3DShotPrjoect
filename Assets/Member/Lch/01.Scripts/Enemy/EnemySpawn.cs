using DG.Tweening;
using Member.Ysc._01_Code.UI;
using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private EnemySpawnListSO enemySpawnSO;
    private float _currentSpawnTime;
    private int _enemySpawnCount;
    [SerializeField] private GameProgressCheckUI gameProgressCheckUI;

    private void Update()
    {
        _currentSpawnTime += Time.deltaTime;
        if (_currentSpawnTime >= enemySpawnSO.SpawnTimer && enemySpawnSO.StageEnemyCount != _enemySpawnCount)
        {
            StartCoroutine(SpawnEnemy());
            _currentSpawnTime = 0;
        }
    }

    public IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemySpawnSO.SpawnCount; i++)
        {
            int randIndex = Random.Range(0, enemySpawnSO.enemies.Count);
            Enemy enemy = Instantiate(enemySpawnSO.enemies[randIndex], transform.position, Quaternion.identity);
            _enemySpawnCount += 1;
            Debug.Log(_enemySpawnCount);
            if(_enemySpawnCount >= enemySpawnSO.StageEnemyCount)
            {
                _enemySpawnCount = enemySpawnSO.StageEnemyCount;
            }
            enemy.transform.SetParent(mainCamera.transform);
            enemy.OnRealDead.AddListener(gameProgressCheckUI.HandleEnemyDeadCount);
            yield return new WaitForSeconds(1.5f);
            continue;
        }
    }
}
