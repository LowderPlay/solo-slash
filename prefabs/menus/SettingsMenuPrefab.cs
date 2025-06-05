using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.menu;
using solo_slasher.component.menu.items;
using solo_slasher.component.render;
using solo_slasher.config;

namespace solo_slasher.prefabs.menus;

internal class SettingsController(GraphicsDeviceManager graphics, Action exit) : IMenuController
{
    public int Width => SettingsMenuPrefab.ScreenSize.X; 
    public int Height => ((IMenuController)this).MeasureHeight();

    private bool _fullscreenHover;
    private bool _noEnemyHover;
    private bool _debugHover;
    private bool _exitHover;

    private SoundEffectInstance _soundCheckMusic = Assets.GuitarLick.CreateInstance();
    private SoundEffectInstance _soundCheckEffect = Assets.CoinEffect.CreateInstance();

    private void ToggleFullscreen()
    {
        ConfigManager.Config.Fullscreen = !ConfigManager.Config.Fullscreen;
        graphics.IsFullScreen = ConfigManager.Config.Fullscreen;
        graphics.ApplyChanges();
    }
    
    private static void ToggleDebug()
    {
        if (!EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player)) return;
        
        if (EntityManager.HasComponent<PerformanceTrackerShownComponent>(player))
            EntityManager.RemoveComponent<PerformanceTrackerShownComponent>(player);
        else
            EntityManager.AddComponent(player, new PerformanceTrackerShownComponent());
    }
    
    private static bool HasDebug()
    {
        return EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player) &&
               EntityManager.HasComponent<PerformanceTrackerShownComponent>(player);
    }
    
    public IEnumerable<IMenuItem> GetMenuItems()
    {
        yield return new ImageMenuItem(Assets.Logo, 2f);
        yield return new HeadingMenuItem("Управление");
        yield return new ImageMenuItem(Assets.Controls, 2f);
        yield return new ButtonMenuItem(exit, hover => _exitHover = hover, _exitHover, "Выйти из игры...");
        yield return new HeadingMenuItem("Настройки");
        yield return new CheckboxMenuItem(
            (_, _) => ToggleFullscreen(), hover => _fullscreenHover = hover, 
            ConfigManager.Config.Fullscreen, _fullscreenHover, "Полный экран");
        yield return new CheckboxMenuItem(
            (_, _) => ConfigManager.Config.DisabledEnemies = !ConfigManager.Config.DisabledEnemies, hover => _noEnemyHover = hover, 
            ConfigManager.Config.DisabledEnemies, _noEnemyHover, "Без врагов");
        yield return new CheckboxMenuItem(
            (_, _) => ToggleDebug(), hover => _debugHover = hover, 
            HasDebug(), _debugHover, "Производительность");
        yield return new HeadingMenuItem("Громкость");
        yield return new SliderMenuItem(
            (value) =>
            {
                ConfigManager.Config.MusicVolume = value / Width;
                // if (_soundCheckMusic.Volume == ConfigManager.Config.MusicVolume) return;
                _soundCheckMusic.Volume = ConfigManager.Config.MusicVolume;
                _soundCheckMusic.Play();
            }, ConfigManager.Config.MusicVolume, 
            _ => {}, false, "Музыка");
        yield return new SliderMenuItem(
            (value) =>
            {
                ConfigManager.Config.SoundVolume = value / Width;
                // if (_soundCheckEffect.Volume == ConfigManager.Config.SoundVolume) return;
                _soundCheckEffect.Volume = ConfigManager.Config.SoundVolume;
                _soundCheckEffect.Play();
            }, ConfigManager.Config.SoundVolume, 
            _ => {}, false, "Эффекты");
    }
}

public static class SettingsMenuPrefab
{
    private static readonly Point ScreenPosition = new Point(14, 11) * new Point(2);
    internal static readonly Point ScreenSize = new Point(105, 91) * new Point(2);
    private static Rectangle _screenRect;
    private static readonly Vector2 Alignment = new(0.5f, 0.9f);
    
    public static void Create(GraphicsDeviceManager graphics, Action exit)
    {
        var offset = new Vector2(Assets.Settings.Width, Assets.Settings.Height) * Alignment * 2;
        _screenRect = new Rectangle((-offset).ToPoint() + ScreenPosition, ScreenSize);
        
        var settings = EntityManager.CreateEntity();
        var controller = new SettingsController(graphics, exit);
        EntityManager.AddComponent(settings, new PositionComponent { Position = new Vector2(0, -100) });
        EntityManager.AddComponent(settings, new UiStructureComponent
        {
            Controller = controller
        });
        EntityManager.AddComponent(settings, new MouseControllableComponent
        {
            HitBox = _screenRect,
            ScrollRange = (Math.Min(-controller.Height + ScreenSize.Y, 0), 0),
            ScrollScale = 32,
        });
        EntityManager.AddComponent(settings, new RenderPipelineComponent(RenderPipeline));
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        var mouseControllable = EntityManager.GetComponent<MouseControllableComponent>(entity);

        yield return new TextureOperation
        {
            Alignment = Alignment,
            Sheet = Assets.Settings,
            InternalScale = 2f,
        };
        
        yield return new ScrollableInternalCanvasOperation(-mouseControllable.Scroll, _screenRect);
    }
}
