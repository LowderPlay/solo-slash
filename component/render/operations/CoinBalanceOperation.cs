using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.config;

namespace solo_slasher.component.render;

public class CoinBalanceOperation : IRenderOperation
{
    public void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects,
        float rotation)
    {
        var coinSprite = new SpritesheetOperation
        {
            Sheet = Assets.Coin,
            Size = (8, 1),
            Alignment = new Vector2(0.5f, 0.5f),
            InternalScale = 3
        };
        var coinSize = coinSprite.SpriteSize * coinSprite.InternalScale;
        position += new Vector2(coinSize.X, -coinSize.Y);
        coinSprite.RenderSprite(spriteBatch, Vector2.Zero, position, scale, tint, effects, rotation);
        var text = $"{ConfigManager.Config.CoinBalance}";
        var textSize = Assets.BalanceFont.MeasureString(text);
        var textPos = position + new Vector2(coinSize.X, -textSize.Y / 2);
        spriteBatch.DrawString(Assets.BalanceFont, text, textPos, new Color(0xFF, 0xD0, 0x2C));
    }
}
