using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.component.notes;
using solo_slasher.component.render;
using solo_slasher.prefabs;

namespace solo_slasher.system.notes;

public class KeyboardHitCheckSystem
{
    private KeyboardState _oldState;
    public void Update(GameTime gameTime)
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
        if (_oldState.IsKeyUp(note.Key) && newState.IsKeyDown(note.Key) && distance < Constants.NoteIgnoreDistance)
        {
            Console.WriteLine(distance);

            if (EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player))
            {
                if (EntityManager.TryGetComponent<HealthComponent>(player, out var healthComponent))
                    healthComponent.AddHealth(5);
                
                var playerPosition = EntityManager.GetComponent<PositionComponent>(player).Position;
                foreach (var target in EntityManager.GetEntitiesWith(typeof(EnemyAiComponent), typeof(PositionComponent)))
                {
                    var enemyPosition = EntityManager.GetComponent<PositionComponent>(target).Position;
                    if ((playerPosition - enemyPosition).Length() < 800)
                        ProjectilePrefab.Create(gameTime, playerPosition, target);
                }
            }
            EntityManager.AddComponent(entity, new HitNoteComponent { Distance = distance });
            EntityManager.AddComponent(entity, new TintComponent
            {
                TintColor = Color.Lerp(Color.White, Color.Green, Constants.NoteWidth / distance)
            });
        }
        
        _oldState = newState;
    }
}