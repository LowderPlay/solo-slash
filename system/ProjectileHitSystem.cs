using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.system;

public class ProjectileHitSystem
{
    public void Update()
    {
        if(!EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player)) return;
        var playerPosition = EntityManager.GetComponent<PositionComponent>(player);
        foreach (var entity in EntityManager.GetEntitiesWith(typeof(ProjectileComponent), typeof(PositionComponent)))
        {
            var position = EntityManager.GetComponent<PositionComponent>(entity);
            var projectile = EntityManager.GetComponent<ProjectileComponent>(entity);
            if (!EntityManager.TryGetComponent<PositionComponent>(projectile.Target, out var targetPosition))
            {
                if ((playerPosition.Position - position.Position).Length() > 1280f) EntityManager.DestroyEntity(entity);
                continue;
            }

            if (!((position.Position - targetPosition.Position).Length() < 50)) continue;
            
            if (EntityManager.TryGetComponent<HealthComponent>(projectile.Target, out var healthComponent))
            {
                healthComponent.AddHealth(-projectile.Damage);
                if (healthComponent.Health <= 0)
                {
                    EntityManager.DestroyEntity(projectile.Target);
                }
            }
            EntityManager.DestroyEntity(entity);
        }
    }
}