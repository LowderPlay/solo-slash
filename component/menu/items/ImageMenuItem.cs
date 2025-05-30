using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.menu.items;

public class ImageMenuItem(Texture2D asset, float scale = 1f) : IMenuItem
{
    public int Height => (int)(asset.Height * scale);
    public void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        spriteBatch.Draw(asset, 
            position + new Vector2(width, Height) / 2 - asset.Bounds.Size.ToVector2() * scale / 2, 
            null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
