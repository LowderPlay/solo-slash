using System.Collections.Generic;
using System.IO;

namespace solo_slasher.track.structure;

public class Section
{
    public double DurationBeats;
    public int Repeat;
    public List<Note> Notes;

    public override string ToString()
    {
        return $"{DurationBeats} beats x {Repeat} times\n" +
               $"{string.Join(", ", Notes)}";
    }

    public static Section Read(BinaryReader reader)
    {
        var duration = reader.ReadDouble();
        var repeat = (int) reader.ReadUInt32();
        
        var noteCount = reader.ReadUInt32();
        var notes = new List<Note>();
        for (var i = 0; i < noteCount; i++)
        {
            notes.Add(Note.Read(reader));
        }
        
        return new Section { DurationBeats = duration, Repeat = repeat, Notes = notes };
    }
}