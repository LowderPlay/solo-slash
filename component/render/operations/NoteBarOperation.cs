using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;

namespace solo_slasher.component.render;

public class NoteBarOperation(Rectangle screenSize) : IRenderOperation
{
    public void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects,
        float rotation)
    {
        spriteBatch.Draw(Assets.DuelBar, new Rectangle(0, 0, screenSize.Width, (int)(Assets.DuelBar.Height * scale)), tint);
        spriteBatch.Draw(Assets.Line, new Vector2(Constants.LinePosition, 0), 
            null, tint, 0, Vector2.Zero, scale, effects, rotation);
    }
}
