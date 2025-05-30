using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.duel;
using solo_slasher.track;
using Note = solo_slasher.duel.Note;

namespace solo_slasher.system;

public class DuelStartSystem
{
    public void Update(GameTime gameTime)
    {
        if(!EntityManager.TryGetFirstEntityWith<PlayingTrackComponent>(out var player)) return;
        var track = EntityManager.GetComponent<PlayingTrackComponent>(player);
        if (track.StartTime == null)
        {
            track.StartTime = gameTime.TotalGameTime;
        }
    }

    private TrackState BuildState(Track track)
    {
        var notes = new Queue<Note>();
        double time = 1;
        foreach (var section in track.BeatMap.Sections)
        {
            for (var i = 0; i < section.Repeat; i++)
            {
                foreach (var note in section.Notes)
                {
                    notes.Enqueue(new Note
                    {
                        Key = note.Key,
                        TimeInBeats = time + note.SectionBeat
                    });
                }
                time += section.DurationBeats;
            }
        }

        return new TrackState
        {
            Bpm = track.BeatMap.Bpm,
            Notes = notes,
        };
    }
}
