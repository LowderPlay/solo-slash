using System;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.component.render;
using solo_slasher.duel;
using solo_slasher.prefabs;

namespace solo_slasher.system.notes;

public class NoteSpawnerSystem
{
    public void HandleNoteSpawn(GameTime gameTime, Rectangle viewportBounds)
    {
        if(!EntityManager.TryGetFirstEntityWith<DuelingComponent>(out var player)) return;
        var duel = EntityManager.GetComponent<DuelingComponent>(player);

        var msPerBeat = (double)duel.DuelState.Bpm / (60 * 1000);
        var arriveTime = (gameTime.TotalGameTime - duel.DuelState.StartTime).TotalMilliseconds + Constants.NoteFlyDuration * 1000;
        var beatTime = arriveTime * msPerBeat;
        
        while (duel.DuelState.Notes.TryPeek(out var note) && note.TimeInBeats <= beatTime)
        {
            var lateForMs = (beatTime - note.TimeInBeats) / msPerBeat;
            Console.WriteLine($"Late for {lateForMs}ms");
            NotePrefab.Create(duel.DuelState.Notes.Dequeue(), viewportBounds, (float) lateForMs);
        }
    }
}