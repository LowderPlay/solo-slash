using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.animations;
using solo_slasher.component.render;

namespace solo_slasher.prefabs;

public static class CoinPrefab
{
    public static void Create(Vector2 position)
    {
        var coin = EntityManager.CreateEntity();
        EntityManager.AddComponent(coin, new PositionComponent
        {
            Position = position
        });
        EntityManager.AddComponent(coin, new CoinAnimationComponent());
        EntityManager.AddComponent(coin, new CollectableCoinComponent());
        EntityManager.AddComponent(coin, new RenderPipelineComponent(RenderPipeline));
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        EntityManager.AddComponent(entity, new SheetIndexComponent
        {
            X = EntityManager.GetComponent<CoinAnimationComponent>(entity).GetCurrentFrame(gameTime), 
            Y = 0
        });
        yield return new TextureOperation
        {
            Sheet = Assets.Shadow,
            Alignment = new Vector2(0.5f, 0.5f),
        };
        yield return new SpritesheetOperation
        {
            Sheet = Assets.Coin,
            Size = (8, 1),
            Alignment = new Vector2(0.5f, 1f),
            InternalScale = 3,
        };
    }
}
