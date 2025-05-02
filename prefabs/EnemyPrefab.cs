using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.prefabs;

public static class EnemyPrefab
{
    public static void Create()
    {
        var enemy = EntityManager.CreateEntity();
        EntityManager.AddComponent(enemy, new DuelableComponent());
        EntityManager.AddComponent(enemy, new PositionComponent
        {
            Position = new Vector2(256, 256)
        });
        EntityManager.AddComponent(enemy, new ScaleComponent { Scale = 2f });
        // EntityManager.AddComponent(enemy, new ZOrderComponent { ZOrder = 1 });
        EntityManager.AddComponent(enemy, new RenderPipelineComponent(RenderPipeline));
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        yield return new TextureOperation
        {
            Texture = Assets.Shadow,
            Alignment = new Vector2(0.5f, 0.5f),
        };
        
        yield return new TextureOperation 
        {
            Texture = Assets.Mushroom,
            Alignment = new Vector2(0.5f, 1f)
        };
    }
}