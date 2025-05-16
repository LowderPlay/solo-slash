using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Xna.Framework;
using solo_slasher.component;
using solo_slasher.component.render;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace solo_slasher.system;

public class EnemyAiSystem
{
    public void Update(GameTime gameTime)
    {
        if(!EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player)) return;
        var playerPosition = EntityManager.GetComponent<PositionComponent>(player).Position;

        List<(Entity enemy, float distance)> enemies = [];
        var closeEnemies = 0;
        
        foreach (var enemy in EntityManager.GetEntitiesWith([typeof(EnemyAiComponent), typeof(PositionComponent)]))
        {
            var position = EntityManager.GetComponent<PositionComponent>(enemy).Position;
            var distance = (playerPosition - position).Length();
            
            enemies.Add((enemy, distance));
            if (distance < 1000) closeEnemies++;
        }

        foreach (var (enemy, distance) in enemies)
        {
            var aiState = EntityManager.GetComponent<EnemyAiComponent>(enemy);
            if (distance < aiState.StayDistance && (gameTime.TotalGameTime - aiState.LastHit) > TimeSpan.FromSeconds(1))
            {
                aiState.LastHit = gameTime.TotalGameTime;
                if (EntityManager.TryGetComponent<HealthComponent>(player, out var healthComponent))
                    healthComponent.Health -= 5;
            }
            
            if (distance < aiState.StayDistance || (closeEnemies >= 5 && distance > 1000)) 
                EntityManager.AddComponent(enemy, new VelocityComponent { Velocity = Vector2.Zero });
            else
            {
                var position = EntityManager.GetComponent<PositionComponent>(enemy).Position;
                var velocity = Vector2.Normalize(playerPosition - position) * 180f;
                velocity += Vector2.Rotate(velocity * 2, (float) Math.PI / 2 * (aiState.Noise.Noise(-position) - 0.5f)) * (aiState.Noise.Noise(position) - 0.5f);
                velocity.Round();
                EntityManager.AddComponent(enemy, new VelocityComponent { Velocity = velocity });
            }
        }
    }
}