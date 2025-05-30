using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;

namespace solo_slasher.component.menu.items;

public class ButtonMenuItem(Action click, Action<bool> hover, bool hovered, string name) : HoverableMenuItem((_, _) => click.Invoke(), hover, hovered)
{
    public override int Height => 30;

    public override void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        base.Render(spriteBatch, position, width);
        IMenuItem.DrawBorder(spriteBatch, position, width, Height);

        spriteBatch.DrawString(Assets.MenuFont, name,
            position + new Vector2(width, Height) / 2 - Assets.MenuFont.MeasureString(name) / 2,
            Color.White, 0f, Vector2.Zero,
            1, SpriteEffects.None, 0);
        
    }
}
