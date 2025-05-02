using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.prefabs;

public static class DebugInfoPrefab
{
    public static void Create(PerformanceTracker performanceTracker)
    {
        var debugText = EntityManager.CreateEntity();
        EntityManager.AddComponent(debugText, new ScreenPositionComponent
        {
            Position = Vector2.Zero
        });
        EntityManager.AddComponent(debugText, new ZOrderComponent { ZOrder = 99 });
        EntityManager.AddComponent(debugText, new ScaleComponent { Scale = 0.3f });
        EntityManager.AddComponent(debugText, new RenderPipelineComponent(RenderPipeline));
        EntityManager.AddComponent(debugText, new AttachedPerformanceTrackerComponent
        {
            PerformanceTracker = performanceTracker
        });
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player);
        var position = EntityManager.GetComponent<PositionComponent>(player);
        var tracker = EntityManager.GetComponent<AttachedPerformanceTrackerComponent>(entity);
        var text = $"FPS: {Math.Truncate(tracker.PerformanceTracker.FramesPerSecond * 100) / 100}\n" +
                   $"UPS: {Math.Truncate(tracker.PerformanceTracker.UpdatesPerSecond * 100) / 100}\n" +
                   $"X: {position.Position.X} Y: {position.Position.Y}\n" +
                   $"Entities: {EntityManager.Count}";
        yield return new TextOperation
        {
            Font = Assets.NoteFont,
            Alignment = Vector2.Zero,
            Text = text
        };
    }
}