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
        if(!EntityManager.TryGetFirstEntityWith<PlayingTrackComponent>(out var player)) return;
        var track = EntityManager.GetComponent<PlayingTrackComponent>(player);

        var msPerBeat = (double)track.TrackState.Bpm / (60 * 1000);
        var arriveTime = (gameTime.TotalGameTime - track.StartTime).TotalMilliseconds + Constants.NoteFlyDuration * 1000;
        var beatTime = arriveTime * msPerBeat;
        
        while (track.TrackState.Notes.TryPeek(out var note) && note.TimeInBeats <= beatTime)
        {
            var lateForMs = (beatTime - note.TimeInBeats) / msPerBeat;
            Console.WriteLine($"Late for {lateForMs}ms");
            NotePrefab.Create(track.TrackState.Notes.Dequeue(), viewportBounds, (float) lateForMs);
        }
    }
}