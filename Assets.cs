using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace rhythm_cs2;

public static class Assets
{
    public static Texture2D Mushroom { get; private set; }
    public static Texture2D Beetroot { get; private set; }
    public static Texture2D Pumpkin { get; private set; }
    public static Texture2D Shadow { get; private set; }
    
    public static Texture2D Grass { get; private set; }
    public static Texture2D Bushes { get; private set; }
    public static Texture2D Fills { get; private set; }
    public static Texture2D Pumpkins { get; private set; }
    public static Texture2D Rocks { get; private set; }
    public static Texture2D Trees { get; private set; }
    
    public static Texture2D Shop { get; private set; }
    public static Texture2D Songs { get; private set; }
    public static Texture2D Settings { get; private set; }
    
    public static Texture2D Bolt { get; private set; }
    public static Texture2D Fireball { get; private set; }
    
    public static Texture2D Coin { get; private set; }
    
    public static Texture2D DuelBar { get; private set; }
    public static Texture2D Line { get; private set; }
    public static Texture2D Box { get; private set; }
    public static SpriteFont NoteFont { get; private set; }
    public static SpriteFont BalanceFont { get; private set; }
    public static SpriteFont TitleFont { get; private set; }
    public static SpriteFont MenuFont { get; private set; }
    public static SpriteFont SliderFont { get; private set; }
    
    public static Texture2D PlayerWalk { get; private set; }
    public static Texture2D PlayerGuitar1 { get; private set; }
    public static Texture2D PlayerGuitar2 { get; private set; }
    public static Texture2D PlayerGuitar3 { get; private set; }
    public static Texture2D PlayerShirtBlack { get; private set; }
    public static Texture2D PlayerShirtGreen { get; private set; }
    public static Texture2D PlayerShirtPink { get; private set; }
    public static Texture2D PlayerShirtWhite { get; private set; }
    public static Texture2D PlayerPantsGray { get; private set; }
    public static Texture2D PlayerPantsWhite { get; private set; }
    
    public static Texture2D Checkbox { get; private set; }
    public static Texture2D Checkmark { get; private set; }
    public static Texture2D Logo { get; private set; }
    public static Texture2D Controls { get; private set; }
    
    public static SoundEffect Click { get; private set; }
    public static SoundEffect CoinEffect { get; private set; }
    public static SoundEffect Hit { get; private set; }
    public static SoundEffect GuitarLick { get; private set; }
    public static SoundEffect Missed { get; private set; }
    
    public static Texture2D Solid { get; private set; }

    public static void LoadAssets(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        Mushroom = contentManager.Load<Texture2D>("assets/entities/mushroom");
        Beetroot = contentManager.Load<Texture2D>("assets/entities/beet");
        Pumpkin = contentManager.Load<Texture2D>("assets/entities/pumpkin");
        Shadow = contentManager.Load<Texture2D>("assets/entities/shadow");
        
        Grass = contentManager.Load<Texture2D>("assets/map/grass");
        Bushes = contentManager.Load<Texture2D>("assets/map/bushes");
        Fills = contentManager.Load<Texture2D>("assets/map/fills");
        Pumpkins = contentManager.Load<Texture2D>("assets/map/pumpkins");
        Rocks = contentManager.Load<Texture2D>("assets/map/rocks");
        Trees = contentManager.Load<Texture2D>("assets/map/trees");
        
        Shop = contentManager.Load<Texture2D>("assets/objects/shop");
        Songs = contentManager.Load<Texture2D>("assets/objects/song");
        Settings = contentManager.Load<Texture2D>("assets/objects/settings");
        
        Bolt = contentManager.Load<Texture2D>("assets/projectiles/bolt");
        Fireball = contentManager.Load<Texture2D>("assets/projectiles/fireball");
        Coin = contentManager.Load<Texture2D>("assets/coin");
        
        DuelBar = contentManager.Load<Texture2D>("assets/duel_bar");
        Line = contentManager.Load<Texture2D>("assets/line");
        Box = contentManager.Load<Texture2D>("assets/box");
        
        NoteFont = contentManager.Load<SpriteFont>("fonts/note");
        BalanceFont = contentManager.Load<SpriteFont>("fonts/balance");
        TitleFont = contentManager.Load<SpriteFont>("fonts/title");
        MenuFont = contentManager.Load<SpriteFont>("fonts/menu");
        SliderFont = contentManager.Load<SpriteFont>("fonts/slider");
        
        PlayerWalk = contentManager.Load<Texture2D>("assets/player/walk");
        PlayerGuitar1 = contentManager.Load<Texture2D>("assets/player/clothing/guitars/guitar1");
        PlayerGuitar2 = contentManager.Load<Texture2D>("assets/player/clothing/guitars/guitar2");
        PlayerGuitar3 = contentManager.Load<Texture2D>("assets/player/clothing/guitars/guitar3");
        PlayerShirtBlack = contentManager.Load<Texture2D>("assets/player/clothing/shirts/black");
        PlayerShirtGreen = contentManager.Load<Texture2D>("assets/player/clothing/shirts/green");
        PlayerShirtPink = contentManager.Load<Texture2D>("assets/player/clothing/shirts/pink");
        PlayerShirtWhite = contentManager.Load<Texture2D>("assets/player/clothing/shirts/white");
        PlayerPantsGray = contentManager.Load<Texture2D>("assets/player/clothing/pants/gray");
        PlayerPantsWhite = contentManager.Load<Texture2D>("assets/player/clothing/pants/white");
        
        Checkbox = contentManager.Load<Texture2D>("assets/ui/checkbox");
        Checkmark = contentManager.Load<Texture2D>("assets/ui/checkmark");
        Logo = contentManager.Load<Texture2D>("assets/logo");
        Controls = contentManager.Load<Texture2D>("assets/controls");
        
        Click = contentManager.Load<SoundEffect>("assets/sounds/click");
        CoinEffect = contentManager.Load<SoundEffect>("assets/sounds/coin");
        Hit = contentManager.Load<SoundEffect>("assets/sounds/hit");
        GuitarLick = contentManager.Load<SoundEffect>("assets/sounds/lick");
        Missed = contentManager.Load<SoundEffect>("assets/sounds/missed");
        
        Solid = CreateSolidTexture(graphicsDevice, Color.White);
    }

    private static Texture2D CreateSolidTexture(GraphicsDevice graphicsDevice, Color color)
    {
        var texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData([color]);
        return texture;
    }
}
