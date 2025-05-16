using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.component.animations;

namespace solo_slasher.system;

public class KeyboardMovementSystem
{
    public void Update(GameTime gameTime)
    {
        foreach (var entity in EntityManager.GetEntitiesWith(
                     typeof(KeyboardControllableComponent), typeof(PositionComponent)))
        {
            var keyboardInput = EntityManager.GetComponent<KeyboardControllableComponent>(entity);
            var position = EntityManager.GetComponent<PositionComponent>(entity);

            var delta = Vector2.Zero;
            if(Keyboard.GetState().IsKeyDown(keyboardInput.DownKey))
                delta += Vector2.UnitY;
            if(Keyboard.GetState().IsKeyDown(keyboardInput.UpKey))
                delta -= Vector2.UnitY;
            if(Keyboard.GetState().IsKeyDown(keyboardInput.LeftKey))
                delta -= Vector2.UnitX;
            if(Keyboard.GetState().IsKeyDown(keyboardInput.RightKey))
                delta += Vector2.UnitX;

            if (delta == Vector2.Zero)
            {
                EntityManager.RemoveComponent<PlayerWalkingAnimationComponent>(entity);
                return;
            }
            
            delta.Normalize();
            
            if (delta.X < 0)
            {
                EntityManager.AddComponent(entity, new LookingDirectionComponent { Direction = LookDirection.Left });
            } 
            else if (delta.X > 0)
            {
                EntityManager.AddComponent(entity, new LookingDirectionComponent { Direction = LookDirection.Right });
            }

            if(!EntityManager.HasComponent<PlayerWalkingAnimationComponent>(entity))
                EntityManager.AddComponent(entity, new PlayerWalkingAnimationComponent(gameTime.TotalGameTime));
            
            position.Position += delta * keyboardInput.StepsPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Position.Round();
        }
    }
}