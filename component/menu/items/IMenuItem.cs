using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;

namespace solo_slasher.component.menu.items;

public interface IMenuItem
{
    public int Height { get; }
    public void Render(SpriteBatch spriteBatch, Vector2 position, int width);
    public void OnClick(MouseButton button, Vector2 position) {}
    public void OnHold(Vector2 position) {}
    public void OnHover(bool isOver) {}
    
    internal static void DrawBorder(SpriteBatch spriteBatch, Vector2 position, int width, int height) {
        spriteBatch.Draw(Assets.Solid, 
            new Rectangle((position + Vector2.UnitY * (height - 1)).ToPoint(), new Point(width, 1)), 
            new Color(Color.Black, 0.6f));
    }
}
