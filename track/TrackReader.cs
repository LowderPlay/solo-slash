using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using solo_slasher.track.structure;

namespace solo_slasher.track;

public class TrackReader
{
    public static Track ReadFile(GraphicsDevice graphicsDevice, string path)
    {
        using var file = new FileStream(path, FileMode.Open);
        using var zip = new ZipArchive(file, ZipArchiveMode.Read);
        var trackStream = new MemoryStream();
        zip.GetEntry("track.wav")?.Open().CopyTo(trackStream);
        trackStream.Position = 0;
        var sound = SoundEffect.FromStream(trackStream);

        var coverStream = new MemoryStream();
        zip.GetEntry("cover.jpg")?.Open().CopyTo(coverStream);
        coverStream.Position = 0;
        var cover = Texture2D.FromStream(graphicsDevice, coverStream);
        return new Track
        {
            BeatMap = ReadFromStream(zip.GetEntry("map.dat")?.Open()),
            Song = sound,
            Cover = cover,
        };
    }
    
    private static BeatMap ReadFromStream(Stream input)
    {
        using var reader = new BinaryReader(input);
        return BeatMap.Read(reader);
    }
}
