using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class SpritesheetOperation : IRenderOperation
{
    public Texture2D Sheet;
    public (int width, int height) Size = (1, 1);
    public Vector2 Alignment = new(0f, 0f);
    
    public void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects)
    {
        var sourceSize = new Vector2(Sheet.Width / Size.width, Sheet.Height / Size.height);
        
        var spriteSize = (EntityManager.TryGetComponent<SizeComponent>(entity, out var size) ? 
            size.Size : sourceSize) * scale;
        
        var index = EntityManager.TryGetComponent<SheetIndexComponent>(entity, out var i) ? 
            new Vector2(i.X, i.Y) : Vector2.Zero;

        var alignedLocation = position - spriteSize * Alignment;
        spriteBatch.Draw(Sheet, new Rectangle(alignedLocation.ToPoint(), spriteSize.ToPoint()), 
            new Rectangle((sourceSize * index).ToPoint(), sourceSize.ToPoint()), tint, 0f, 
            Vector2.Zero, effects, 1f);
    }
    
}