using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.prefabs;
using solo_slasher.system;
using solo_slasher.system.notes;
using solo_slasher.system.render;

namespace solo_slasher;

public class Game1 : Game
{
    private PerformanceTracker _performanceTracker;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderSystem _renderSystem;
    private RenderPipelineBuilderSystem _renderPipelineBuilderSystem;
    
    private MapFillerSystem _mapFillerSystem;
    
    private KeyboardMovementSystem _keyboardMovementSystem;
    private DuelStartSystem _duelStartSystem;
    private UiSystem _uiSystem;
    private VelocityMoveSystem _velocityMoveSystem;
    
    private NoteSpawnerSystem _noteSpawnerSystem;
    private NoteMissSystem _noteMissSystem;
    private KeyboardHitCheckSystem _keyboardHitCheckSystem;

    public Game1()
    {
        _performanceTracker = new PerformanceTracker();
        _graphics = new GraphicsDeviceManager(this);
        // TODO: move to settings?
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;

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
        _velocityMoveSystem = new VelocityMoveSystem();
        _noteMissSystem = new NoteMissSystem();
        _uiSystem = new UiSystem();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Assets.LoadAssets(Content, GraphicsDevice);
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // MapPrefab.Create();
        DebugInfoPrefab.Create(_performanceTracker);
        PlayerPrefab.Create();
        EnemyPrefab.Create();

        _uiSystem.Initialize(GraphicsDevice.Viewport.Bounds);
        _renderSystem = new RenderSystem(_spriteBatch);
        _renderPipelineBuilderSystem = new RenderPipelineBuilderSystem();
    }

    protected override void Update(GameTime gameTime)
    {
        _performanceTracker.SetUpdateDelta(gameTime.ElapsedGameTime.TotalSeconds);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mapFillerSystem.Update();
        _keyboardMovementSystem.Update(gameTime);
        _keyboardHitCheckSystem.Update();
        _duelStartSystem.Update(gameTime);
        _velocityMoveSystem.Update(gameTime);
        _uiSystem.Update(gameTime);
        _noteMissSystem.Update();
        _renderPipelineBuilderSystem.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _performanceTracker.SetDrawDelta(gameTime.ElapsedGameTime.TotalSeconds);
        GraphicsDevice.Clear(Color.ForestGreen);

        _renderSystem.Render(GraphicsDevice.Viewport.Bounds, gameTime);
        _noteSpawnerSystem.HandleNoteSpawn(gameTime, GraphicsDevice.Viewport.Bounds);

        base.Draw(gameTime);
    }
}
