using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnList", menuName = "SO/EnemySpawnList")]
public class EnemySpawnListSO : ScriptableObject
{
    public List<Enemy> enemies;
    [TextArea]
    public string discription;
    public int SpawnCount = 0;
    public float SpawnTimer = 0;
    public int StageEnemyCount = 0;
}
