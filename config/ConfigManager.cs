using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using rhythm_cs2;

namespace solo_slasher.config;

public class Config()
{
    public float MusicVolume { get; set; } = 1;
    public float SoundVolume { get; set; } = 1;
    public bool Fullscreen { get; set; }
    public bool DisabledEnemies { get; set; }
    
    public string PickedShirt { get; set; } = Assets.PlayerShirtBlack.Name;
    public string PickedPants { get; set; } = Assets.PlayerPantsGray.Name;
    public string PickedGuitar { get; set; } = Assets.PlayerGuitar1.Name;
    public int CoinBalance { get; set; }
    
    public HashSet<string> AcquiredItems { get; set; } = [
        Assets.PlayerShirtBlack.Name, Assets.PlayerPantsGray.Name, Assets.PlayerGuitar1.Name
    ];

    public override string ToString()
    {
        return $"MusicVolume: {MusicVolume}, SoundVolume: {SoundVolume}";
    }
}

public static class ConfigManager
{
    public static Config Config { get; private set; } = new();

    private static string ConfigFolderPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "solo-slash");

    private static string ConfigFilePath => Path.Combine(ConfigFolderPath, "config.json");
    private static string TracksFolderPath => Path.Combine(ConfigFolderPath, "tracks");
    private static string FactoryTracksPath => Path.Combine("Content", "tracks");

    public static IEnumerable<string> GetTracks()
    {
        Directory.CreateDirectory(TracksFolderPath);
        return Directory.EnumerateFiles(TracksFolderPath);
    }

    private static void PreloadTracks()
    {
        var existingTracks = new HashSet<string>(GetTracks().Select(Path.GetFileName));
        foreach (var missing in Directory.GetFiles(FactoryTracksPath)
                     .Where(x => !existingTracks.Contains(Path.GetFileName(x))))
        {
            Console.WriteLine($"Installing track {missing}");
            File.Copy(missing, Path.Join(TracksFolderPath,  Path.GetFileName(missing)));
        }
    }

    public static void Load()
    {
        if(!File.Exists(ConfigFilePath)) Save();
        
        var stream = File.OpenRead(ConfigFilePath);
        Config = (Config) JsonSerializer.Deserialize(stream, typeof(Config))!;
        Console.WriteLine($"Loaded {Config}");
        stream.Close();
        
        PreloadTracks();
    }

    public static void Save()
    {
        Directory.CreateDirectory(ConfigFolderPath);
        var stream = File.Open(ConfigFilePath, FileMode.Create, FileAccess.Write);
        JsonSerializer.Serialize(stream, Config);
        stream.Close();
    }
}
