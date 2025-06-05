using System;
using System.Linq;
using Microsoft.Xna.Framework;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.config;
using solo_slasher.prefabs;

namespace solo_slasher.system;

public class EnemySpawnSystem
{
    private readonly Random _random = new();
    
    public void Update()
    {
        if(ConfigManager.Config.DisabledEnemies) return;
        if(!EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player)) return;
        if(!EntityManager.HasComponent<PlayingTrackComponent>(player)) return;
        var playerPosition = EntityManager.GetComponent<PositionComponent>(player);
        var enemyCount = EntityManager.GetEntitiesWith<EnemyAiComponent>().ToList().Count;
        if(enemyCount >= Constants.MaxEnemies) return;
        
        for (var i = 0; i < Constants.MaxEnemies - enemyCount; i++)
            EnemyPrefab.Create(playerPosition.Position + 
                               Vector2.Rotate(
                                   Vector2.UnitX, 
                                   (float)(_random.NextDouble() * Math.PI * 2)) * _random.Next(1000, 2000));
    }
    
}
