using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;

namespace solo_slasher.component.menu.items;

public class HeadingMenuItem(string text, Vector2? padding = null) : IMenuItem
{
    private readonly Vector2 _padding = padding ?? new Vector2(5, 3);
    public int Height => (int)(Assets.MenuFont.MeasureString(text).Y + _padding.Y * 2);
    public void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        spriteBatch.DrawString(Assets.MenuFont, text, position + _padding, Color.White);
        IMenuItem.DrawBorder(spriteBatch, position, width, Height);
    }
}
