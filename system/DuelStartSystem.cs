using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.duel;

namespace solo_slasher.system;

public class DuelStartSystem
{
    public void Update(GameTime gameTime)
    {
        if(!EntityManager.TryGetFirstEntityWith<KeyboardControllableComponent>(out var player)) return;
        var playerPosition = EntityManager.GetComponent<PositionComponent>(player);
        
        if (EntityManager.TryGetComponent<DuelingComponent>(player, out var duel))
        {
            var opponentPosition = EntityManager.GetComponent<PositionComponent>(duel.DuelWith);
            if (Vector2.Distance(opponentPosition.Position, playerPosition.Position) > 350)
            {
                EntityManager.RemoveComponent<DuelingComponent>(player);
                foreach (var note in EntityManager.GetEntitiesWith<NoteComponent>())
                {
                    EntityManager.DestroyEntity(note);
                }
            } 
            else return;
        }
        
        foreach (var enemy in EntityManager.GetEntitiesWith(typeof(DuelableComponent), typeof(PositionComponent)))
        {
            var enemyPosition = EntityManager.GetComponent<PositionComponent>(enemy).Position;
            if (Vector2.Distance(playerPosition.Position, enemyPosition) < 300)
            {
                EntityManager.AddComponent(player, new DuelingComponent
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