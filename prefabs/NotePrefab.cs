using System.Collections.Generic;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.component.render;
using solo_slasher.duel;

namespace solo_slasher.prefabs;

public static class NotePrefab
{
    public static void Create(Note noteInfo, Rectangle viewportBounds, float lateForMs)
    {
        var velocity = (viewportBounds.Width - Constants.LinePosition) / Constants.NoteFlyDuration;
        var spawnPosition = viewportBounds.Width - lateForMs / 1000 * velocity;
        
        var note = EntityManager.CreateEntity();
        EntityManager.AddComponent(note, new ZOrderComponent { ZOrder = 3 });
        EntityManager.AddComponent(note, new ScaleComponent { Scale = 0.8f });
        EntityManager.AddComponent(note, new ScreenPositionComponent
        {
            Position = new Vector2(spawnPosition, 55f)
        });
        
        EntityManager.AddComponent(note, new RenderPipelineComponent((time, entity) => RenderPipeline(noteInfo)));
        
        
        EntityManager.AddComponent(note, new VelocityComponent { Velocity = new Vector2(-velocity, 0) });
        EntityManager.AddComponent(note, new NoteComponent { Key = noteInfo.Key });
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(Note noteInfo)
    {
        yield return new TextureOperation
        {
            Texture = Assets.Box,
            Alignment = new Vector2(0.5f, 0.5f)
        };
        yield return new TextOperation
        {
            Text = noteInfo.Key.ToString(), 
            Font = Assets.NoteFont
        };
    }
}