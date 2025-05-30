using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;

namespace solo_slasher.component.menu.items;

public class SliderMenuItem(Action<float> select, float value, Action<bool> hover, bool hovered, string name) : HoverableMenuItem((_, _) => {}, hover, hovered)
{
    public override int Height => 40;

    public override void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        base.Render(spriteBatch, position, width);
        
        var fill = new Rectangle(position.ToPoint(), new Point((int)(width * value), Height));
        spriteBatch.Draw(Assets.Solid, fill, new Color(Color.Green, 0.8f));
        IMenuItem.DrawBorder(spriteBatch, position, width, Height);

        var valueText = $"{Math.Round(value * 100)}%";
        spriteBatch.DrawString(Assets.SliderFont, valueText,
            position + new Vector2(5, Height / 2 - Assets.SliderFont.MeasureString(valueText).Y / 2),
            Color.White, 0f, Vector2.Zero,
            1, SpriteEffects.None, 0);
        
        spriteBatch.DrawString(Assets.MenuFont, name,
            position + new Vector2(Assets.SliderFont.MeasureString(valueText).X + 25, 
                Height / 2 - Assets.MenuFont.MeasureString(name).Y / 2),
            Color.White, 0f, Vector2.Zero,
            1, SpriteEffects.None, 0);
    }

    public override void OnHold(Vector2 position)
    {
        select.Invoke(position.X);
    }
}
