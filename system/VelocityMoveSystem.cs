using Microsoft.Xna.Framework;
using solo_slasher.component;

namespace solo_slasher.system.notes;

public class VelocityMoveSystem(EntityManager entityManager)
{
    public void Update(GameTime gameTime)
    {
        foreach (var entity in entityManager.GetEntitiesWith<VelocityComponent>())
        {
            var velocity = entityManager.GetComponent<VelocityComponent>(entity);
            if (entityManager.TryGetComponent<PositionComponent>(entity, out var positionComponent))
            {
                positionComponent.Position += velocity.Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            } 
            else if (entityManager.TryGetComponent<ScreenPositionComponent>(entity, out var screenPosition))
            {
                screenPosition.Position += velocity.Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            } 
        }
    }
}