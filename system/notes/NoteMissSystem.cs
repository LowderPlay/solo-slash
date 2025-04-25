using Microsoft.Xna.Framework;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.component.render;

namespace solo_slasher.system.notes;

public class NoteMissSystem(EntityManager entityManager)
{
    public void Update()
    {
        foreach (var entity in entityManager.GetEntitiesWith(typeof(NoteComponent), typeof(ScreenPositionComponent)))
        {
            var position = entityManager.GetComponent<ScreenPositionComponent>(entity);

            if (entityManager.HasComponent<HitNoteComponent>(entity) ||
                !(position.Position.X < Constants.LinePosition - Constants.NoteWidth)) continue;
            entityManager.AddComponent(entity, new MissedNoteComponent());
            entityManager.AddComponent(entity, new TintComponent {TintColor = new Color(Color.Red, 0.5f)});
        }
    }
}