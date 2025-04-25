using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.duel;

namespace solo_slasher.system;

public class DuelStartSystem(EntityManager entityManager)
{
    public void Update(GameTime gameTime)
    {
        if(!entityManager.TryGetFirstEntityWith<KeyboardControllableComponent>(out var player)) return;
        var playerPosition = entityManager.GetComponent<PositionComponent>(player);
        
        if (entityManager.TryGetComponent<DuelingComponent>(player, out var duel))
        {
            var opponentPosition = entityManager.GetComponent<PositionComponent>(duel.DuelWith);
            if (Vector2.Distance(opponentPosition.Position, playerPosition.Position) > 150)
            {
                entityManager.RemoveComponent<DuelingComponent>(player);
                foreach (var note in entityManager.GetEntitiesWith<NoteComponent>())
                {
                    entityManager.DestroyEntity(note);
                }
            } 
            else return;
        }
        
        foreach (var enemy in entityManager.GetEntitiesWith(typeof(DuelableComponent), typeof(PositionComponent)))
        {
            var enemyPosition = entityManager.GetComponent<PositionComponent>(enemy).Position;
            if (Vector2.Distance(playerPosition.Position, enemyPosition) < 100)
            {
                entityManager.AddComponent(player, new DuelingComponent
                {
                    DuelWith = enemy,
                    DuelState = new DuelState
                    {
                        StartTime = gameTime.TotalGameTime,
                        Notes = new Queue<Note>([
                            new Note { Key = Keys.U, TimeInBeats = 2 },
                            new Note { Key = Keys.I, TimeInBeats = 4 },
                            new Note { Key = Keys.O, TimeInBeats = 6 },
                            new Note { Key = Keys.P, TimeInBeats = 8 },
                        ])
                    }
                });
            }
        }
    }
}