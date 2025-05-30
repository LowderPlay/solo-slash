using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class ScrollableInternalCanvasOperation(int scroll, Rectangle output) : IRenderOperation
{
    public void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects,
        float rotation)
    {
        var sourceRectangle = output;
        sourceRectangle.X = 0;
        sourceRectangle.Y = scroll;
        if (EntityManager.TryGetComponent<InternalCanvasComponent>(entity, out var canvasComponent))
        {
            var destinationRectangle = output;
            if (canvasComponent.Size.Y < sourceRectangle.Height)
            {
                destinationRectangle.Height = sourceRectangle.Height = Math.Min(canvasComponent.Size.Y, sourceRectangle.Height);    
            }
        
            destinationRectangle.Location *= new Point((int) scale, (int) scale);
            destinationRectangle.Size *= new Point((int) scale, (int) scale);
            destinationRectangle.Location += position.ToPoint();
            spriteBatch.Draw(canvasComponent.Target, destinationRectangle, sourceRectangle, tint, rotation, Vector2.Zero, effects, 1f);
        }
    }
}
