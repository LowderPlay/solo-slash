using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.system;
using solo_slasher.system.notes;

namespace solo_slasher;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private EntityManager _entityManager;
    private RenderSystem _renderSystem;
    private KeyboardMovementSystem _keyboardMovementSystem;
    private DuelStartSystem _duelStartSystem;
    private UiSystem _uiSystem;
    private VelocityMoveSystem _velocityMoveSystem;
    
    private NoteSpawnerSystem _noteSpawnerSystem;
    private NoteMissSystem _noteMissSystem;
    private KeyboardHitCheckSystem _keyboardHitCheckSystem;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        // TODO: move to settings?
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _entityManager = new EntityManager();
        _keyboardMovementSystem = new KeyboardMovementSystem(_entityManager);
        _keyboardHitCheckSystem = new KeyboardHitCheckSystem(_entityManager);
        _duelStartSystem = new DuelStartSystem(_entityManager);
        _noteSpawnerSystem = new NoteSpawnerSystem(_entityManager);
        _velocityMoveSystem = new VelocityMoveSystem(_entityManager);
        _noteMissSystem = new NoteMissSystem(_entityManager);
        _uiSystem = new UiSystem(_entityManager);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        AssetsManager.LoadAssets(Content, GraphicsDevice);
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var player = _entityManager.CreateEntity();
        _entityManager.AddComponent(player, new PositionComponent());
        _entityManager.AddComponent(player, new ScaleComponent { Scale = 4f });
        _entityManager.AddComponent(player, new ZOrderComponent { ZOrder = 1 });
        _entityManager.AddComponent(player, new TextureComponent
        {
            Texture = AssetsManager.Player,
            Alignment = new Vector2(0.5f, 0.5f)
        });
        _entityManager.AddComponent(player, new KeyboardControllableComponent
        {
            StepMultiplier = 4,
        });
        _entityManager.AddComponent(player, new CameraOriginComponent());
        
        var background = _entityManager.CreateEntity();
        _entityManager.AddComponent(background, new ScaleComponent { Scale = 4f });
        _entityManager.AddComponent(background, new PositionComponent
        {
            Position = -new Vector2(AssetsManager.Background.Width * 2, AssetsManager.Background.Height * 2)
        });
        _entityManager.AddComponent(background, new TextureComponent
        {
            Texture = AssetsManager.Background,
        });
        
        var enemy = _entityManager.CreateEntity();
        _entityManager.AddComponent(enemy, new DuelableComponent());
        _entityManager.AddComponent(enemy, new PositionComponent
        {
            Position = new Vector2(128, 128)
        });
        _entityManager.AddComponent(enemy, new ScaleComponent { Scale = 4f });
        _entityManager.AddComponent(enemy, new ZOrderComponent { ZOrder = 1 });
        _entityManager.AddComponent(enemy, new TextureComponent
        {
            Texture = AssetsManager.Enemy,
            Alignment = new Vector2(0.5f, 0.5f)
        });

        _uiSystem.Initialize(GraphicsDevice.Viewport.Bounds);
        _renderSystem = new RenderSystem(_entityManager, _spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _keyboardMovementSystem.Update();
        _keyboardHitCheckSystem.Update();
        _duelStartSystem.Update(gameTime);
        _velocityMoveSystem.Update(gameTime);
        _uiSystem.Update(gameTime);
        _noteMissSystem.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _renderSystem.Render(GraphicsDevice.Viewport.Bounds, gameTime);
        _noteSpawnerSystem.HandleNoteSpawn(gameTime, GraphicsDevice.Viewport.Bounds);

        base.Draw(gameTime);
    }
}
