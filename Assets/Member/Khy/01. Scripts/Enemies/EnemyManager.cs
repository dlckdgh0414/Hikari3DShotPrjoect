using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private static List<Enemy> _enemies = new();

    public static IReadOnlyList<Enemy> Enemies => _enemies;

    public static void Register(Enemy enemy)
    {
        if (!_enemies.Contains(enemy))
            _enemies.Add(enemy);
    }

    public static void Unregister(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}
