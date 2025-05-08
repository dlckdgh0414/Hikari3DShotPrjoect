using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private static List<GameObject> _enemies = new();

    public static IReadOnlyList<GameObject> Enemies => _enemies;

    public static void Register(GameObject enemy)
    {
        if (!_enemies.Contains(enemy))
            _enemies.Add(enemy);
    }

    public static void Unregister(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }
}
