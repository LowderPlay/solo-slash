using System.Collections.Generic;
using System.IO;
using System.Text;

namespace solo_slasher.track.structure;

public class BeatMap
{
    public string Song;
    public string Artist;
    public double Bpm;
    public List<Section> Sections;

    public override string ToString()
    {
        return $"{Artist} - {Song} [{Bpm} BPM]\n" +
               $"{string.Join("\n", Sections)}";
    }

    public static BeatMap Read(BinaryReader reader)
    {
        var songLen = reader.ReadUInt32();
        var song = Encoding.UTF8.GetString(reader.ReadBytes((int) songLen));
        var artistLen = reader.ReadUInt32();
        var artist = Encoding.UTF8.GetString(reader.ReadBytes((int) artistLen));
        var bpm = reader.ReadDouble();
        var sectionsCount = reader.ReadUInt32();
        var sections = new List<Section>();
        for (var i = 0; i < sectionsCount; i++)
        {
            sections.Add(Section.Read(reader));
        }

        return new BeatMap
        {
            Song = song,
            Artist = artist,
            Bpm = bpm,
            Sections = sections
        };
    }
}