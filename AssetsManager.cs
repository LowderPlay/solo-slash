using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace rhythm_cs2;

public static class AssetsManager
{
    public static Texture2D Player;
    public static Texture2D Enemy;
    public static Texture2D Background;
    public static Texture2D DuelBar;
    public static Texture2D Box;
    public static SpriteFont NoteFont;


    public static void LoadAssets(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        Player = contentManager.Load<Texture2D>("assets/player");
        Enemy = contentManager.Load<Texture2D>("assets/enemy");
        Background = contentManager.Load<Texture2D>("assets/background");
        DuelBar = contentManager.Load<Texture2D>("assets/duel_bar");
        Box = contentManager.Load<Texture2D>("assets/box");
        
        NoteFont = contentManager.Load<SpriteFont>("fonts/note");
    }

}