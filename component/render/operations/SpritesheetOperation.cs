using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class SpritesheetOperation : IRenderOperation
{
    public Texture2D Sheet;
    public (int width, int height) Size = (1, 1);
    public Vector2 Alignment = new(0f, 0f);
    public Vector2 SpriteSize => new(Sheet.Width / Size.width, Sheet.Height / Size.height);
    public float InternalScale = 1f;
    
    public virtual void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects, float rotation)
    {
        var index = EntityManager.TryGetComponent<SheetIndexComponent>(entity, out var i) ? 
            new Vector2(i.X, i.Y) : Vector2.Zero;
        RenderSprite(spriteBatch, index, position, scale, tint, effects, rotation);
    }

    public void RenderSprite(SpriteBatch spriteBatch, Vector2 index, Vector2 position, float scale, Color tint,
        SpriteEffects effects, float rotation)
    {
        scale *= InternalScale;
        var spriteSize = SpriteSize * scale;

        var alignedLocation = position - spriteSize * Alignment;
        alignedLocation.Round();
        spriteBatch.Draw(Sheet, new Rectangle(alignedLocation.ToPoint(), spriteSize.ToPoint()), 
            new Rectangle((SpriteSize * index).ToPoint(), SpriteSize.ToPoint()), tint, rotation, 
            Vector2.Zero, effects, 1f);
    }
}
