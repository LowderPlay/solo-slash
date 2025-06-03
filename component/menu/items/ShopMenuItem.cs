using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.component.render;
using solo_slasher.config;

namespace solo_slasher.component.menu.items;

public class ShopMenuItem(CosmeticItem item) : SelectableSpriteMenuItem(
    (_, _) => item.Select(), 
    hover => item.Hovered = hover,
    item.Selected, 
    item.Hovered, 
    new SpritesheetOperation
    {
        Sheet = item.Texture,
        Alignment = new Vector2(0.5f, 0.5f),
        Size = (6, 2)
    }, new Vector2(3, 0), item.Name)
{
    public override void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        base.Render(spriteBatch, position, width);
        
        if(ConfigManager.Config.AcquiredItems.Contains(item.Texture.Name)) return;

        var coin = new SpritesheetOperation
        {
            Sheet = Assets.Coin,
            Size = (8, 1),
            Alignment = new Vector2(0.5f, 0.5f),
            InternalScale = 2,
        };

        position += new Vector2(width - coin.SpriteSize.X - 2, Height / 2);
        
        coin.RenderSprite(spriteBatch, Vector2.Zero, position, 1f, Color.White, SpriteEffects.None, 0f);

        var textSize = Assets.MenuFont.MeasureString($"{item.Price}");
        position -= new Vector2(textSize.X + coin.SpriteSize.X, textSize.Y / 2);
        
        spriteBatch.DrawString(Assets.MenuFont, $"{item.Price}", 
            position, Color.Orange, 0f, Vector2.Zero, 
            1, SpriteEffects.None, 0);
        
    }
}
