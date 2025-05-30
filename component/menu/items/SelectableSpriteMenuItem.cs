using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.component.render;

namespace solo_slasher.component.menu.items;

public class SelectableSpriteMenuItem(Action<MouseButton, Vector2> click, Action<bool> hover,
    bool selected, bool hovered, 
    SpritesheetOperation sprite, Vector2 index, string name) : HoverableMenuItem(click, hover, hovered)
{
    private const float Scale = 0.5f;
    public override int Height => (int)(sprite.SpriteSize.Y * Scale);

    public override void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        base.Render(spriteBatch, position, width);
        
        var button = new Rectangle(position.ToPoint(), new Point(width, Height));
        if (selected)
            spriteBatch.Draw(Assets.Solid, button, new Color(Color.Green, 0.8f));
        
        IMenuItem.DrawBorder(spriteBatch, position, width, Height);
        
        sprite.RenderSprite(spriteBatch, 
            index, position + new Vector2(Height / 2), Scale, Color.White,
            SpriteEffects.None, 0);
        
        spriteBatch.DrawString(Assets.MenuFont, name, 
            position + new Vector2(Height, Height / 2 - Assets.MenuFont.MeasureString(name).Y / 2), 
            Color.White, 0f, Vector2.Zero, 
            1, SpriteEffects.None, 0);
    }
}
