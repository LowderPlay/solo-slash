using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.component.render;
using solo_slasher.duel;

namespace solo_slasher.system.notes;

public class NoteSpawnerSystem(EntityManager entityManager)
{
    
    public void HandleNoteSpawn(GameTime gameTime, Rectangle viewportBounds)
    {
        if(!entityManager.TryGetFirstEntityWith<DuelingComponent>(out var player)) return;
        var duel = entityManager.GetComponent<DuelingComponent>(player);
        // if(duel.DuelState.Notes.Count <= 0) return;

        var beatTime = (gameTime.TotalGameTime - duel.DuelState.StartTime).TotalMilliseconds * ((double)duel.DuelState.Bpm / (60 * 1000));
        
        while (duel.DuelState.Notes.TryPeek(out var note) && note.TimeInBeats <= beatTime)
        {
            SpawnNote(duel.DuelState.Notes.Dequeue(), viewportBounds);
        }
    }

    private void SpawnNote(Note noteInfo, Rectangle viewportBounds)
    {
        var note = entityManager.CreateEntity();
        entityManager.AddComponent(note, new ZOrderComponent { ZOrder = 3 });
        entityManager.AddComponent(note, new TextureComponent
        {
            Texture = AssetsManager.Box,
            Alignment = new Vector2(0.5f, 0.5f)
        });
        entityManager.AddComponent(note, new ScaleComponent { Scale = 0.8f });
        entityManager.AddComponent(note, new TextComponent { Text = noteInfo.Key.ToString(), Font = AssetsManager.NoteFont});
        entityManager.AddComponent(note, new ScreenPositionComponent
        {
            Position = new Vector2(viewportBounds.Width, 55f)
        });
        entityManager.AddComponent(note, new VelocityComponent { Velocity = new Vector2(-400, 0) });
        entityManager.AddComponent(note, new NoteComponent { Key = noteInfo.Key });
    }
}