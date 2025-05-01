using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class TextOperation : IRenderOperation
{
    public SpriteFont Font;
    public string Text;
    public Vector2 Alignment = new(0.5f, 0.5f);
    
    public void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects)
    {
        var textSize = Font.MeasureString(Text) * scale;
                
        var alignedLocation = position - textSize * Alignment;
        spriteBatch.DrawString(Font, Text, alignedLocation, tint, 
            0f, Vector2.Zero, scale, effects, 1f);
    }
}