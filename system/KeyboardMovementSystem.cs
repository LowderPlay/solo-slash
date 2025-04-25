using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;

namespace solo_slasher.system;

public class KeyboardMovementSystem(EntityManager entityManager)
{
    public void Update()
    {
        foreach (var entity in entityManager.GetEntitiesWith(
                     typeof(KeyboardControllableComponent), typeof(PositionComponent)))
        {
            var keyboardInput = entityManager.GetComponent<KeyboardControllableComponent>(entity);
            var position = entityManager.GetComponent<PositionComponent>(entity);

            var delta = Vector2.Zero;
            if(Keyboard.GetState().IsKeyDown(keyboardInput.DownKey))
                delta += Vector2.UnitY;
            if(Keyboard.GetState().IsKeyDown(keyboardInput.UpKey))
                delta -= Vector2.UnitY;
            if(Keyboard.GetState().IsKeyDown(keyboardInput.LeftKey))
                delta -= Vector2.UnitX;
            if(Keyboard.GetState().IsKeyDown(keyboardInput.RightKey))
                delta += Vector2.UnitX;

            if (delta.X < 0)
            {
                entityManager.AddComponent(entity, new MovementFlipComponent { Flip = true });
            } else if (delta.X > 0)
            {
                entityManager.AddComponent(entity, new MovementFlipComponent { Flip = false });
            }
            
            position.Position += delta * keyboardInput.StepMultiplier;
        }
    }
}