using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Xna.Framework.Audio;
using solo_slasher.track.structure;

namespace solo_slasher.track;

public class TrackReader
{
    public static Track ReadFile(string path)
    {
        using var file = new FileStream(path, FileMode.Open);
        using var zip = new ZipArchive(file, ZipArchiveMode.Read);
        var stream = new MemoryStream();
        zip.GetEntry("track.wav")?.Open().CopyTo(stream);
        stream.Position = 0;
        var sound = SoundEffect.FromStream(stream);
        return new Track
        {
            BeatMap = ReadFromStream(zip.GetEntry("map.dat")?.Open()),
            Song = sound
        };
    }
    
    private static BeatMap ReadFromStream(Stream input)
    {
        using var reader = new BinaryReader(input);
        return BeatMap.Read(reader);
    }
}