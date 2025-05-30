using Microsoft.Xna.Framework;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.component.render;

namespace solo_slasher.system.notes;

public class NoteMissSystem
{
    public void Update()
    {
        EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player);
        foreach (var entity in EntityManager.GetEntitiesWith(typeof(NoteComponent), typeof(ScreenPositionComponent)))
        {
            if (EntityManager.HasComponent<MissedNoteComponent>(entity) || EntityManager.HasComponent<HitNoteComponent>(entity))
                continue;
            var position = EntityManager.GetComponent<ScreenPositionComponent>(entity);

            if (!(position.Position.X < Constants.LinePosition - Constants.NoteWidth)) continue;
            EntityManager.AddComponent(entity, new MissedNoteComponent());
            EntityManager.AddComponent(entity, new TintComponent {TintColor = new Color(Color.Red, 0.5f)});
            EntityManager.GetComponent<HealthComponent>(player).AddHealth(-5);
        }
    }
}