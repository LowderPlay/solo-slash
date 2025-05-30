using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.animations;
using solo_slasher.component.render;

namespace solo_slasher.prefabs;

public static class EnemyPrefab
{
    private static readonly Random Random = new();
    public static void Create(Vector2 position)
    {
        var enemy = EntityManager.CreateEntity();
        EntityManager.AddComponent(enemy, new PositionComponent
        {
            Position = position
        });
        EntityManager.AddComponent(enemy, new ScaleComponent { Scale = 2f });
        EntityManager.AddComponent(enemy, new EnemyAiComponent
        {
            Noise = new PerlinNoise((int) enemy.Id), 
            StayDistance = Random.Next(100, 300)
        });
        EntityManager.AddComponent(enemy, new SheetIndexComponent { X = 0, Y = 0 });
        EntityManager.AddComponent(enemy, new EnemyTypeComponent
        {
            EnemyType = Random.NextDouble() > 0.5 ? EnemyType.Beetroot : EnemyType.Mushroom
        });
        EntityManager.AddComponent(enemy, new RenderPipelineComponent(RenderPipeline));
        EntityManager.AddComponent(enemy, new HealthComponent());
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        if (EntityManager.TryGetComponent<SheetIndexComponent>(entity, out var sheetIndex) && 
            EntityManager.TryGetComponent<VelocityComponent>(entity, out var velocityComponent))
        {
            switch (velocityComponent.Velocity.X)
            {
                case > 0:
                    EntityManager.RemoveComponent<EffectsComponent>(entity);
                    break;
                case < 0:
                    EntityManager.AddComponent(entity, new EffectsComponent { Effects = SpriteEffects.FlipHorizontally });
                    break;
            }

            if (velocityComponent.Velocity != Vector2.Zero)
            {
                if (!EntityManager.HasComponent<EnemyWalkingAnimationComponent>(entity))
                    EntityManager.AddComponent(entity, new EnemyWalkingAnimationComponent(gameTime.TotalGameTime));
            }
            else
            {
                EntityManager.RemoveComponent<EnemyWalkingAnimationComponent>(entity);
            }

            sheetIndex.X = EntityManager.TryGetComponent<EnemyWalkingAnimationComponent>(entity, out var walking) 
                ? walking.GetCurrentFrame(gameTime) : 0;
        }
        yield return new TextureOperation
        {
            Sheet = Assets.Shadow,
            Alignment = new Vector2(0.5f, 0.5f),
        };

        var sprite = EntityManager.GetComponent<EnemyTypeComponent>(entity).EnemyType == EnemyType.Beetroot
            ? Assets.Beetroot
            : Assets.Mushroom;
        
        yield return new SpritesheetOperation 
        {
            Sheet = sprite,
            Size = (4, 1),
            Alignment = new Vector2(0.5f, 1f)
        };
        
        yield return new HealthBarOperation();
    }
}
