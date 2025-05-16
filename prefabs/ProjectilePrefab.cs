using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.prefabs;

public class ProjectilePrefab
{
    public static void Create(GameTime spawnTime, Vector2 position, Entity target)
    {
        var projectile = EntityManager.CreateEntity();
        EntityManager.AddComponent(projectile, new ScaleComponent { Scale = 2f });
        // EntityManager.AddComponent(projectile, new ZOrderComponent { ZOrder = 2 });
        EntityManager.AddComponent(projectile, new PositionComponent { Position = position });
        EntityManager.AddComponent(projectile, new ProjectileComponent(spawnTime.TotalGameTime, target));
        EntityManager.AddComponent(projectile, new SheetIndexComponent { X = 0, Y = 0 });
        EntityManager.AddComponent(projectile, new RenderPipelineComponent(RenderPipeline));
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        var projectile = EntityManager.GetComponent<ProjectileComponent>(entity);
        var position = EntityManager.GetComponent<PositionComponent>(entity);

        if (EntityManager.TryGetComponent<PositionComponent>(projectile.Target, out var targetPosition))
        {
            var direction = Vector2.Normalize(targetPosition.Position - position.Position);
            EntityManager.AddComponent(entity, new RotationComponent
            {
                Radians = (float) (Math.Atan2(direction.Y, direction.X))
            });
            EntityManager.AddComponent(entity, new VelocityComponent
            {
                Velocity = direction * projectile.Velocity,
            });
        }
        
        if (EntityManager.TryGetComponent<SheetIndexComponent>(entity, out var sheetIndex))
        {
            sheetIndex.X = projectile.GetCurrentFrame(gameTime);
        }
        yield return new SpritesheetOperation 
        {
            Sheet = Assets.Fireball,
            Size = (6, 1),
            Alignment = new Vector2(0.5f, 0.5f)
        };
    }
}