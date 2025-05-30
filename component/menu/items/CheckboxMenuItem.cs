using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.component.render;

namespace solo_slasher.component.menu.items;

public class CheckboxMenuItem(
    Action<MouseButton, Vector2> click,
    Action<bool> hover,
    bool selected,
    bool hovered,
    string name) : HoverableMenuItem(click, hover, hovered)
{
    public override int Height => 40;

    private readonly TextureOperation _checkboxOperation = new()
    {
        Sheet = Assets.Checkbox,
        Alignment = new Vector2(0.5f, 0.5f)
    };

    private readonly TextureOperation _checkmarkOperation = new()
    {
        Sheet = Assets.Checkmark,
        Alignment = new Vector2(0.5f, 0.5f)
    };

    public override void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        base.Render(spriteBatch, position, width);
        IMenuItem.DrawBorder(spriteBatch, position, width, Height);

        _checkboxOperation.RenderSprite(spriteBatch, position + new Vector2(Height / 2), 2f, Color.White,
            SpriteEffects.None, 0);
        if(selected)
            _checkmarkOperation.RenderSprite(spriteBatch, position + new Vector2(Height / 2), 2f, Color.White,
                SpriteEffects.None, 0);

        spriteBatch.DrawString(Assets.MenuFont, name,
            position + new Vector2(Height + 5, Height / 2 - Assets.MenuFont.MeasureString(name).Y / 2),
            Color.White, 0f, Vector2.Zero,
            1, SpriteEffects.None, 0);
    }
}
