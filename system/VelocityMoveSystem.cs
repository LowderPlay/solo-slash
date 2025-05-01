using Microsoft.Xna.Framework;
using solo_slasher.component;

namespace solo_slasher.system.notes;

public class VelocityMoveSystem
{
    public void Update(GameTime gameTime)
    {
        foreach (var entity in EntityManager.GetEntitiesWith<VelocityComponent>())
        {
            var velocity = EntityManager.GetComponent<VelocityComponent>(entity);
            if (EntityManager.TryGetComponent<PositionComponent>(entity, out var positionComponent))
            {
                positionComponent.Position += velocity.Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            } 
            else if (EntityManager.TryGetComponent<ScreenPositionComponent>(entity, out var screenPosition))
            {
                screenPosition.Position += velocity.Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            } 
        }
    }
}