using System.IO;
using Microsoft.Xna.Framework.Input;

namespace solo_slasher.track.structure;

public class Note
{
    public double SectionBeat;
    public Keys Key;

    public override string ToString()
    {
        return $"[{SectionBeat}] {Key}";
    }

    public static Note Read(BinaryReader reader)
    {
        var beat = reader.ReadDouble();
        var key = reader.ReadChar();

        return new Note { SectionBeat = beat, Key = (Keys) key };
    }
}