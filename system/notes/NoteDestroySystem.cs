using solo_slasher.component;
using solo_slasher.component.notes;

namespace solo_slasher.system.notes;

public class NoteDestroySystem
{
    public void CheckNoteDestroy()
    {
        foreach (var entity in EntityManager.GetEntitiesWith(typeof(NoteComponent), typeof(ScreenPositionComponent)))
        {
            var position = EntityManager.GetComponent<ScreenPositionComponent>(entity);
            if (position.Position.X >= 0) continue;
            EntityManager.DestroyEntity(entity);
        }
    }
}