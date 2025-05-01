using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace rhythm_cs2;

public static class Assets
{
    public static Texture2D Player;
    public static Texture2D Mushroom;
    public static Texture2D Beet;
    public static Texture2D Background;
    public static Texture2D DuelBar;
    public static Texture2D Box;
    public static SpriteFont NoteFont;

    public static void LoadAssets(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        Player = contentManager.Load<Texture2D>("assets/entities/player");
        Mushroom = contentManager.Load<Texture2D>("assets/entities/mushroom");
        Beet = contentManager.Load<Texture2D>("assets/entities/beet");
        
        Background = contentManager.Load<Texture2D>("assets/background");
        DuelBar = contentManager.Load<Texture2D>("assets/duel_bar");
        Box = contentManager.Load<Texture2D>("assets/box");
        
        NoteFont = contentManager.Load<SpriteFont>("fonts/note");
    }
}