using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.config;
using solo_slasher.duel;
using solo_slasher.track;
using Note = solo_slasher.duel.Note;

namespace solo_slasher.system;

public class TrackStateSystem
{
    public void Update(GameTime gameTime, Action buildMenus)
    {
        if (!EntityManager.TryGetFirstEntityWith<PlayingTrackComponent>(out var player))
            return;

        var track = EntityManager.GetComponent<PlayingTrackComponent>(player);
        var health = EntityManager.GetComponent<HealthComponent>(player);
        if (track.StartTime == null)
        {
            track.StartTime = gameTime.TotalGameTime + track.BeforePlaying;
            EntityManager.AddComponent(player, new ScreenFadeEffectComponent(track.BeforePlaying.TotalMilliseconds, 0, 500)
            {
                StartTime = gameTime.TotalGameTime
            });
        }
        else if (track.SoundEffect.State != SoundState.Playing && track.StartTime <= gameTime.TotalGameTime)
        {
            Start(track);
        } else if(track.StartTime + track.Duration <= gameTime.TotalGameTime 
                  || health.Health <= 0 
                  || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            EntityManager.AddComponent(player, new ScreenFadeEffectComponent(0, 500, 500)
            {
                StartTime = gameTime.TotalGameTime,
                Color = health.Health <= 0 ? Color.Red : Color.Black,
            });
            track.SoundEffect.Stop();
            End(player, buildMenus);
        }
    }

    private void End(Entity player, Action buildMenus)
    {
        EntityManager.RemoveComponent<PlayingTrackComponent>(player);
        EntityManager.AddComponent(player, new PositionComponent { Position = Vector2.Zero});
        EntityManager.AddComponent(player, new HealthComponent());
        foreach (var entity in EntityManager.GetEntitiesWithAny(typeof(EnemyAiComponent), typeof(NoteComponent), 
                     typeof(CollectableCoinComponent), typeof(ProjectileComponent)))
        {
            EntityManager.DestroyEntity(entity);
        }
        buildMenus.Invoke();
    }

    private void Start(PlayingTrackComponent track)
    {
        foreach (var entity in EntityManager.GetEntitiesWith<UiStructureComponent>())
        {
            EntityManager.DestroyEntity(entity);
        }

        track.SoundEffect.Volume = ConfigManager.Config.MusicVolume;
        track.SoundEffect.Play();
    }
}
