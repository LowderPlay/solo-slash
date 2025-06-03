using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.component.render.pipelines;

namespace solo_slasher.system;

public static class CoinBalancePrefab
{
    public static void Create(Rectangle screenSize)
    {
        var duelBar = EntityManager.CreateEntity();
        EntityManager.AddComponent(duelBar, new ScreenPositionComponent
        {
            Position = new Vector2(0, screenSize.Height),
        });
        EntityManager.AddComponent(duelBar, new RenderPipelineComponent(RenderPipeline));
        EntityManager.AddComponent(duelBar, new ZOrderComponent { ZOrder = 2 });
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        yield return new CoinBalanceOperation();
    }
}
