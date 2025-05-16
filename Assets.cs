using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace rhythm_cs2;

public static class Assets
{
    public static Texture2D Player { get; private set; }
    public static Texture2D Mushroom { get; private set; }
    public static Texture2D Beetroot { get; private set; }
    public static Texture2D Shadow { get; private set; }
    
    public static Texture2D Grass { get; private set; }
    public static Texture2D Bushes { get; private set; }
    public static Texture2D Fills { get; private set; }
    public static Texture2D Pumpkins { get; private set; }
    public static Texture2D Rocks { get; private set; }
    public static Texture2D Trees { get; private set; }
    
    public static Texture2D Bolt { get; private set; }
    public static Texture2D Fireball { get; private set; }
    
    public static Texture2D DuelBar { get; private set; }
    public static Texture2D Box { get; private set; }
    public static SpriteFont NoteFont { get; private set; }
    
    public static Texture2D Solid { get; private set; }

    public static void LoadAssets(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        Player = contentManager.Load<Texture2D>("assets/entities/player");
        Mushroom = contentManager.Load<Texture2D>("assets/entities/mushroom");
        Beetroot = contentManager.Load<Texture2D>("assets/entities/beet");
        Shadow = contentManager.Load<Texture2D>("assets/entities/shadow");
        
        Grass = contentManager.Load<Texture2D>("assets/map/grass");
        Bushes = contentManager.Load<Texture2D>("assets/map/bushes");
        Fills = contentManager.Load<Texture2D>("assets/map/fills");
        Pumpkins = contentManager.Load<Texture2D>("assets/map/pumpkins");
        Rocks = contentManager.Load<Texture2D>("assets/map/rocks");
        Trees = contentManager.Load<Texture2D>("assets/map/trees");
        
        Bolt = contentManager.Load<Texture2D>("assets/projectiles/bolt");
        Fireball = contentManager.Load<Texture2D>("assets/projectiles/fireball");
        
        DuelBar = contentManager.Load<Texture2D>("assets/duel_bar");
        Box = contentManager.Load<Texture2D>("assets/box");
        
        NoteFont = contentManager.Load<SpriteFont>("fonts/note");
        
        Solid = CreateSolidTexture(graphicsDevice, Color.White);
    }

    private static Texture2D CreateSolidTexture(GraphicsDevice graphicsDevice, Color color)
    {
        var texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData([color]);
        return texture;
    }
}