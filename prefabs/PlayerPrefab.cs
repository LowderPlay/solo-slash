using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.animations;
using solo_slasher.component.render;

namespace solo_slasher.prefabs;

public static class PlayerPrefab
{
    public static void Create()
    {
        var player = EntityManager.CreateEntity();
        EntityManager.AddComponent(player, new PositionComponent());
        EntityManager.AddComponent(player, new ScaleComponent { Scale = 2f });
        // EntityManager.AddComponent(player, new ZOrderComponent { ZOrder = 1 });
        EntityManager.AddComponent(player, new SheetIndexComponent { X = 0, Y = 0 });
        EntityManager.AddComponent(player, new RenderPipelineComponent(RenderPipeline));
        
        EntityManager.AddComponent(player, new KeyboardControllableComponent { StepsPerSecond = 250 });
        EntityManager.AddComponent(player, new CameraOriginComponent());
        EntityManager.AddComponent(player, new HealthComponent());
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        if (EntityManager.TryGetComponent<SheetIndexComponent>(entity, out var sheetIndex))
        {
            if (EntityManager.TryGetComponent<LookingDirectionComponent>(entity, out var look))
            {
                sheetIndex.X = look.Direction == LookDirection.Left ? 0 : 1;
            }

            if (EntityManager.TryGetComponent<PlayerWalkingAnimationComponent>(entity, out var walking))
            {
                sheetIndex.Y = 1 + walking.GetCurrentFrame(gameTime);
            }
            else
            {
                sheetIndex.Y = 0;
            }
        }

        yield return new TextureOperation
        {
            Texture = Assets.Shadow,
            Alignment = new Vector2(0.5f, 0.5f),
        };
        
        yield return new SpritesheetOperation
        {
            Sheet = Assets.Player,
            Size = (2, 3),
            Alignment = new Vector2(0.5f, 1f)
        };
        yield return new HealthBarOperation();
    }
}