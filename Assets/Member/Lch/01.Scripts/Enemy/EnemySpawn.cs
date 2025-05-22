using Member.Ysc._01_Code.UI;
using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private EnemySpawnListSO enemySpawnSO;
    private float _currentSpawnTime;
    private int _currentSpawnEnemy;
    
    [SerializeField] private GameProgressCheckUI gameProgressCheckUI;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
       StartCoroutine(SpawnEnemy());
    }
    private void Update()
    {
        _currentSpawnTime += Time.deltaTime;
        if (_currentSpawnTime >= enemySpawnSO.SpawnTimer && _currentSpawnEnemy != enemySpawnSO.StageEnemyCount)
        {
            StartCoroutine(SpawnEnemy());
            _currentSpawnTime = 0;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemySpawnSO.SpawnCount; i++)
        {
            int randIndex = Random.Range(0, enemySpawnSO.enemies.Count);
            var enemy = PoolManager.Instance.Pop(enemySpawnSO.enemies[randIndex].name);
            GameObject enemyObj = enemy.GetGameObject();
            enemyObj.transform.SetParent(mainCamera.transform);
            enemyObj.transform.position = transform.position;
            enemyObj.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (enemy.GetGameObject().TryGetComponent(out Enemy e))
            {
                e.OnRealDead.AddListener(gameProgressCheckUI.HandleEnemyDeadCount);
            }
            _currentSpawnEnemy++;
            if (_currentSpawnEnemy == enemySpawnSO.StageEnemyCount)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
