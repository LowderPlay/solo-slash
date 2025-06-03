using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.animations;
using solo_slasher.component.render;
using solo_slasher.config;

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
        var hasGuitar = EntityManager.HasComponent<PlayingTrackComponent>(entity);
        if (EntityManager.TryGetComponent<SheetIndexComponent>(entity, out var sheetIndex))
        {
            if (EntityManager.TryGetComponent<LookingDirectionComponent>(entity, out var look))
            {
                sheetIndex.Y = look.Direction == LookDirection.Left ? 0 : 1;
            }

            sheetIndex.X = hasGuitar ? 3 : 0;
            if (EntityManager.TryGetComponent<PlayerWalkingAnimationComponent>(entity, out var walking))
            {
                var frame = walking.GetCurrentFrame(gameTime);
                sheetIndex.X += frame % 2 == 0 ? 0 : (frame + 1) / 2;
            }
            else
            {
                sheetIndex.X += 0;
            }
        }
        yield return new TextureOperation
        {
            Sheet = Assets.Shadow,
            Alignment = new Vector2(0.5f, 0.5f),
        };
        
        yield return new SpritesheetOperation
        {
            Sheet = Assets.PlayerWalk,
            Size = (6, 2),
            Alignment = new Vector2(0.5f, 0.9f)
        };
        
        yield return new SpritesheetOperation
        {
            Sheet = Cosmetics.Pants[ConfigManager.Config.PickedPants].Texture,
            Size = (6, 2),
            Alignment = new Vector2(0.5f, 0.9f)
        };
        
        yield return new SpritesheetOperation
        {
            Sheet = Cosmetics.Shirts[ConfigManager.Config.PickedShirt].Texture,
            Size = (6, 2),
            Alignment = new Vector2(0.5f, 0.9f)
        };

        if (hasGuitar)
        {
            yield return new SpritesheetOperation
            {
                Sheet = Cosmetics.Guitars[ConfigManager.Config.PickedGuitar].Texture,
                Size = (6, 2),
                Alignment = new Vector2(0.5f, 0.9f)
            };
        }
        
        yield return new HealthBarOperation();
    }
}
