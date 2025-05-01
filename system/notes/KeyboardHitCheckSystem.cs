using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.component.render;

namespace solo_slasher.system.notes;

public class KeyboardHitCheckSystem
{
    private KeyboardState _oldState;
    public void Update()
    {
        var entity = EntityManager.GetEntitiesWith(typeof(NoteComponent), typeof(ScreenPositionComponent))
            .Where(entity => !EntityManager.HasComponent<MissedNoteComponent>(entity)
                             && !EntityManager.HasComponent<HitNoteComponent>(entity))
            .OrderBy(entity => EntityManager.GetComponent<ScreenPositionComponent>(entity).Position.X)
            .FirstOrDefault();

        if (entity == null)
            return;
        
        var newState = Keyboard.GetState();
        
        var position = EntityManager.GetComponent<ScreenPositionComponent>(entity);
        var note = EntityManager.GetComponent<NoteComponent>(entity);

        var distance = Math.Abs(position.Position.X - Constants.LinePosition);
        if (_oldState.IsKeyUp(note.Key) && newState.IsKeyDown(note.Key) && distance < Constants.NoteIgnoreDistance && newState.GetPressedKeyCount() == 1)
        {
            Console.WriteLine(distance);
        
            EntityManager.AddComponent(entity, new HitNoteComponent { Distance = distance });
            EntityManager.AddComponent(entity, new TintComponent
            {
                TintColor = Color.Lerp(Color.White, Color.Green, Constants.NoteWidth / distance)
            });
        }
        
        _oldState = newState;
    }
}