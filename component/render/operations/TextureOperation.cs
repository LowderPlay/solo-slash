using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class TextureOperation : IRenderOperation
{
    public Texture2D Texture;
    public Vector2 Alignment = new(0f, 0f);
    
    public void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects, float rotation)
    {
        var spriteSize = (EntityManager.TryGetComponent<SizeComponent>(entity, out var size) ? 
            size.Size : new Vector2(Texture.Width, Texture.Height)) * scale;

        var alignedLocation = position - spriteSize * Alignment;
        spriteBatch.Draw(Texture, new Rectangle(alignedLocation.ToPoint(), spriteSize.ToPoint()), 
            null, tint, rotation, Vector2.Zero, effects, 1f);
    }
}