using solo_slasher.component;
using solo_slasher.component.notes;

namespace solo_slasher.system.notes;

public class NoteDestroySystem(EntityManager entityManager)
{
    public void CheckNoteDestroy()
    {
        foreach (var entity in entityManager.GetEntitiesWith(typeof(NoteComponent), typeof(ScreenPositionComponent)))
        {
            var position = entityManager.GetComponent<ScreenPositionComponent>(entity);
            if (position.Position.X >= 0) continue;
            entityManager.DestroyEntity(entity);
        }
    }
}