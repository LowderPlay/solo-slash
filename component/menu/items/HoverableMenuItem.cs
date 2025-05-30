using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;

namespace solo_slasher.component.menu.items;

public abstract class HoverableMenuItem(Action<MouseButton, Vector2> click, Action<bool> hover, bool hovered) : BindableMenuItem(click, hover, (_) => {})
{
    public override void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        var button = new Rectangle(position.ToPoint(), new Point(width, Height));
        if (hovered)
            spriteBatch.Draw(Assets.Solid, button, new Color(Color.White, 0.3f));
    }
}
