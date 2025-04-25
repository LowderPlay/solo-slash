using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.component.render;

namespace solo_slasher.system.notes;

public class KeyboardHitCheckSystem(EntityManager entityManager)
{
    private KeyboardState _oldState;
    public void Update()
    {
        var entity = entityManager.GetEntitiesWith(typeof(NoteComponent), typeof(ScreenPositionComponent))
            .Where(entity => !entityManager.HasComponent<MissedNoteComponent>(entity)
                             && !entityManager.HasComponent<HitNoteComponent>(entity))
            .OrderBy(entity => entityManager.GetComponent<ScreenPositionComponent>(entity).Position.X)
            .FirstOrDefault();

        if (entity == null)
            return;
        
        var newState = Keyboard.GetState();
        
        var position = entityManager.GetComponent<ScreenPositionComponent>(entity);
        var note = entityManager.GetComponent<NoteComponent>(entity);

        var distance = Math.Abs(position.Position.X - Constants.LinePosition);
        if (_oldState.IsKeyUp(note.Key) && newState.IsKeyDown(note.Key) && distance < Constants.NoteIgnoreDistance && newState.GetPressedKeyCount() == 1)
        {
            Console.WriteLine(distance);
            distance = distance < Constants.NoteWidth ? 5 : distance;
        
            entityManager.AddComponent(entity, new HitNoteComponent { Distance = distance });
            entityManager.AddComponent(entity, new TintComponent
            {
                TintColor = new Color(Color.Green, 1 / distance)
            });
        }
        
        _oldState = newState;
    }
}