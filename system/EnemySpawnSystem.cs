using System;
using System.Linq;
using Microsoft.Xna.Framework;
using solo_slasher.component;
using solo_slasher.prefabs;

namespace solo_slasher.system;

public class EnemySpawnSystem
{
    private readonly Random _random = new();
    
    public void Update()
    {
        var enemyCount = EntityManager.GetEntitiesWith<EnemyAiComponent>().ToList().Count;
        if(enemyCount >= Constants.MaxEnemies) return;
        
        for (var i = 0; i < Constants.MaxEnemies - enemyCount; i++)
            EnemyPrefab.Create(Vector2.Rotate(Vector2.UnitX, (float)(_random.NextDouble() * Math.PI * 2)) * _random.Next(1000, 2000));
    }
    
}