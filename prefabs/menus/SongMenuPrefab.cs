using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.menu;
using solo_slasher.component.menu.items;
using solo_slasher.component.render;
using solo_slasher.component.render.pipelines;
using solo_slasher.config;
using solo_slasher.duel;
using solo_slasher.track;

namespace solo_slasher.prefabs.menus;

internal class SongController(List<Track> tracks) : IMenuController
{
    public int Width => SongMenuPrefab.ScreenSize.X;
    public int Height => ((IMenuController)this).MeasureHeight();

    private readonly List<bool> _hovered = tracks.Select(_ => false).ToList();
    
    public IEnumerable<IMenuItem> GetMenuItems()
    {
        yield return new HeadingMenuItem("Треки");
        foreach (var (track, i) in tracks.Select((t, i) => (t, i)))
        {
            yield return new SongMenuItem((_, _) =>
            {
                StartTrack(track);
            }, track, hover => _hovered[i] = hover, _hovered[i]);
        }
    }

    private static void StartTrack(Track track)
    {
        if(!EntityManager.TryGetFirstEntityWith<KeyboardControllableComponent>(out var player) || 
           EntityManager.HasComponent<PlayingTrackComponent>(player)) return;
        var notes = new Queue<Note>();
        double time = 0;
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
        
        var song = track.Song.CreateInstance();
        song.IsLooped = true;
        EntityManager.AddComponent(player, new PlayingTrackComponent
        {
            TrackState = new TrackState
            {
                Bpm = track.BeatMap.Bpm,
                Notes = notes,
            },
            Duration = track.Song.Duration,
            SoundEffect = song,
            BeforePlaying = TimeSpan.FromMilliseconds(500)
        });
    }
}

public static class SongMenuPrefab
{
    private static readonly Point ScreenPosition = new Point(0, 3) * new Point(2);
    internal static readonly Point ScreenSize = new Point(142, 92) * new Point(2);
    private static Rectangle _screenRect;
    private static readonly Vector2 Alignment = new(0.5f, 0.9f);
    
    public static void Create(GraphicsDevice graphicsDevice)
    {
        var offset = new Vector2(Assets.Songs.Width, Assets.Songs.Height) * Alignment * 2;
        _screenRect = new Rectangle((-offset).ToPoint() + ScreenPosition, ScreenSize);

        var controller = new SongController(LoadTracks(graphicsDevice));
        
        var songs = EntityManager.CreateEntity();
        EntityManager.AddComponent(songs, new PositionComponent { Position = new Vector2(300, -50) });
        EntityManager.AddComponent(songs, new UiStructureComponent
        {
            Controller = controller,
        });
        EntityManager.AddComponent(songs, new MouseControllableComponent
        {
            HitBox = _screenRect,
            ScrollRange = (Math.Min(-controller.Height + ScreenSize.Y, 0), 0),
            ScrollScale = 32,
        });
        // EntityManager.AddComponent(songs, new ScaleComponent { Scale = 2f });
        EntityManager.AddComponent(songs, new RenderPipelineComponent(RenderPipeline));
    }

    private static List<Track> LoadTracks(GraphicsDevice graphicsDevice)
    {
        return ConfigManager.GetTracks()
            .Select(track => TrackReader.ReadFile(graphicsDevice, track))
            .ToList();
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        var mouseControllable = EntityManager.GetComponent<MouseControllableComponent>(entity);

        yield return new TextureOperation
        {
            Alignment = Alignment,
            Sheet = Assets.Songs,
            InternalScale = 2f,
        };
        
        yield return new ScrollableInternalCanvasOperation(-mouseControllable.Scroll, _screenRect);
    }
}
