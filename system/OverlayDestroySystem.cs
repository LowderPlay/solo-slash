using Microsoft.Xna.Framework;
using solo_slasher.component;

namespace solo_slasher.system;

public class OverlayDestroySystem
{
    public void Update(GameTime gameTime)
    {
        foreach (var entity in EntityManager.GetEntitiesWith<ScreenFadeEffectComponent>())
        {
            var fade = EntityManager.GetComponent<ScreenFadeEffectComponent>(entity);
            if (fade.StartTime.TotalMilliseconds + fade.TotalTime < gameTime.TotalGameTime.TotalMilliseconds)
                EntityManager.RemoveComponent<ScreenFadeEffectComponent>(entity);
        }
    }
}
