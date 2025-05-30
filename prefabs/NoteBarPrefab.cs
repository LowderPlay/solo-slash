using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.component.render.pipelines;

namespace solo_slasher.system;

public static class NoteBarPrefab
{
    public const float Scale = 0.8f;
    
    public static void Create(Rectangle screenSize)
    {
        var duelBar = EntityManager.CreateEntity();
        EntityManager.AddComponent(duelBar, new ScreenPositionComponent
        {
            Position = Vector2.Zero,
        });
        EntityManager.AddComponent(duelBar, new RenderPipelineComponent((time, entity) => RenderPipeline(time, entity, screenSize)));
        EntityManager.AddComponent(duelBar, new ZOrderComponent { ZOrder = 2 });
        EntityManager.AddComponent(duelBar, new ScaleComponent { Scale = Scale });
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity, Rectangle screenSize)
    {
        if (!EntityManager.TryGetFirstEntityWith<KeyboardControllableComponent>(out var player) 
            || !EntityManager.HasComponent<PlayingTrackComponent>(player)) yield break;

        yield return new NoteBarOperation(screenSize);
    }
}
