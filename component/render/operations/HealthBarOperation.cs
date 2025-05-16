using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;

namespace solo_slasher.component.render;

public class HealthBarOperation : IRenderOperation
{
    private readonly Vector2 _healthBarSize = new(50, 3);
    private readonly Vector2 _healthBarOffset = new(0, 5);
    
    public void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, float scale, Color tint, SpriteEffects effects, float rotation)
    {
        if(!EntityManager.TryGetComponent<HealthComponent>(entity, out var health)) return;
        var backgroundRect = new Rectangle((position - _healthBarSize / 2 + _healthBarOffset).ToPoint(), _healthBarSize.ToPoint());
        var healthRect = new Rectangle(
            backgroundRect.Location, 
            new Point((int) (_healthBarSize.X * health.Health / 100), (int)_healthBarSize.Y));
        
        spriteBatch.Draw(Assets.Solid, backgroundRect, Color.Red);
        spriteBatch.Draw(Assets.Solid, healthRect, Color.Green);
    }
}