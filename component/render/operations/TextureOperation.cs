using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class TextureOperation : SpritesheetOperation
{
    public override void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects, float rotation)
    {
        RenderSprite(spriteBatch, Vector2.Zero, position, scale, tint, effects, rotation);
    }
    
    public void RenderSprite(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint, SpriteEffects effects, float rotation)
    {
        base.RenderSprite(spriteBatch, Vector2.Zero, position, scale, tint, effects, rotation);
    }
}
