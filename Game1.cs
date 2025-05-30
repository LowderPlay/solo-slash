using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.component.render.pipelines;
using solo_slasher.prefabs;
using solo_slasher.prefabs.menus;
using solo_slasher.system;
using solo_slasher.system.notes;
using solo_slasher.system.render;

namespace solo_slasher;

public class Game1 : Game
{
    private readonly PerformanceTracker _performanceTracker;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderSystem _renderSystem;
    private RenderPipelineBuilderSystem _renderPipelineBuilderSystem;
    
    private MapFillerSystem _mapFillerSystem;
    
    private KeyboardMovementSystem _keyboardMovementSystem;
    private DuelStartSystem _duelStartSystem;
    private VelocityMoveSystem _velocityMoveSystem;
    
    private NoteSpawnerSystem _noteSpawnerSystem;
    private NoteMissSystem _noteMissSystem;
    private KeyboardHitCheckSystem _keyboardHitCheckSystem;
    private EnemyAiSystem _enemyAiSystem;
    private EnemySpawnSystem _enemySpawnSystem;
    private ProjectileHitSystem _projectileHitSystem;
    private MouseHandlerSystem _mouseHandlerSystem;
    private MenuBuilderSystem _menuBuilderSystem;
    private readonly Rectangle _screenSize;

    public Game1()
    {
        _performanceTracker = new PerformanceTracker();
        _graphics = new GraphicsDeviceManager(this);
        // TODO: move to settings?
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.HardwareModeSwitch = false;
        
        
        _screenSize = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        // IsFixedTimeStep = false;
        // _graphics.SynchronizeWithVerticalRetrace = false;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _mapFillerSystem = new MapFillerSystem();
        _keyboardMovementSystem = new KeyboardMovementSystem();
        _keyboardHitCheckSystem = new KeyboardHitCheckSystem();
        _duelStartSystem = new DuelStartSystem();
        _noteSpawnerSystem = new NoteSpawnerSystem();
        _enemyAiSystem = new EnemyAiSystem();
        _enemySpawnSystem = new EnemySpawnSystem();
        _projectileHitSystem = new ProjectileHitSystem();
        _velocityMoveSystem = new VelocityMoveSystem();
        _noteMissSystem = new NoteMissSystem();
        _mouseHandlerSystem = new MouseHandlerSystem();
        _menuBuilderSystem = new MenuBuilderSystem();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Assets.LoadAssets(Content, GraphicsDevice);
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        DebugInfoPrefab.Create(_performanceTracker);
        PlayerPrefab.Create();

        ShopMenuPrefab.Create();
        SettingsMenuPrefab.Create(_graphics, Exit);
        SongMenuPrefab.Create(GraphicsDevice);
        NoteBarPrefab.Create(GraphicsDevice.Viewport.Bounds);

        _renderSystem = new RenderSystem(_spriteBatch, _screenSize);
        _renderPipelineBuilderSystem = new RenderPipelineBuilderSystem();
    }

    protected override void Update(GameTime gameTime)
    {
        _performanceTracker.SetUpdateDelta(gameTime.ElapsedGameTime.TotalSeconds);
        // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //     Exit();

        _mapFillerSystem.Update();
        _duelStartSystem.Update(gameTime);
        _noteSpawnerSystem.HandleNoteSpawn(gameTime, _screenSize);
        _mouseHandlerSystem.Update(_screenSize, GraphicsDevice.Viewport.Bounds);
        _keyboardMovementSystem.Update(gameTime);
        _keyboardHitCheckSystem.Update(gameTime);
        _enemyAiSystem.Update(gameTime);
        _enemySpawnSystem.Update();
        _projectileHitSystem.Update();
        _velocityMoveSystem.Update(gameTime);
        _noteMissSystem.Update();
        
        _menuBuilderSystem.Update();
        _renderPipelineBuilderSystem.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _performanceTracker.SetDrawDelta(gameTime.ElapsedGameTime.TotalSeconds);

        _renderSystem.Render(GraphicsDevice.Viewport.Bounds, gameTime);

        base.Draw(gameTime);
    }
}
