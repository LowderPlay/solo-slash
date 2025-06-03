using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.system;

public class MouseHandlerSystem
{
    private MouseState _previousMouseState;
    
    public void Update(Rectangle screenSize, Rectangle viewportBounds)
    {
        if(!EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var player)) return;
        var screenOrigin = new Vector2(screenSize.Width / 2f, screenSize.Height / 2f);
        var cameraPosition = EntityManager.GetComponent<PositionComponent>(player).Position;
        var mouseState = Mouse.GetState();
        foreach (var entity in EntityManager.GetEntitiesWith<MouseControllableComponent>())
        {
            var mouseComponent = EntityManager.GetComponent<MouseControllableComponent>(entity);
            var position = EntityManager.GetComponent<PositionComponent>(entity);

            var box = mouseComponent.HitBox;
            var scale = 1f;
            if (EntityManager.TryGetComponent<ScaleComponent>(entity, out var scaleComponent))
            {
                scale = scaleComponent.Scale;
            }
            box.Location *= new Point((int)scale, (int)scale);
            box.Size *= new Point((int)scale, (int)scale);
            box.Location += position.Position.ToPoint() - cameraPosition.ToPoint() + screenOrigin.ToPoint();

            if (mouseState.LeftButton == ButtonState.Released)
            {
                mouseComponent.DraggingFrom = null;
            }

            var screenScale = new Vector2((float) screenSize.Width / viewportBounds.Width, 
                (float) screenSize.Height / viewportBounds.Height);
            var mousePos = mouseState.Position.ToVector2() * screenScale;
            var contains = box.Contains(mousePos);
            if (contains || mouseComponent.DraggingFrom != null)
            {
                var clampedPos = new Vector2(
                    Math.Clamp(mousePos.X, box.Left, box.Right), 
                    Math.Clamp(mousePos.Y, box.Top, box.Bottom));
                mouseComponent.MousePosition = (clampedPos - box.Location.ToVector2()) / scale;
                EntityManager.AddComponent(player, new LookingDirectionComponent
                {
                    Direction = mousePos.X > screenSize.Width / 2 ? LookDirection.Right : LookDirection.Left,
                });
            }

            if (contains)
            {
                mouseComponent.Scroll += (mouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue) / 120 * mouseComponent.ScrollScale;
                if (mouseComponent.Scroll > mouseComponent.ScrollRange.max)
                    mouseComponent.Scroll = mouseComponent.ScrollRange.max;
                if (mouseComponent.Scroll < mouseComponent.ScrollRange.min)
                    mouseComponent.Scroll = mouseComponent.ScrollRange.min;
                mouseComponent.Hovered = true;

                if (mouseState.LeftButton == ButtonState.Pressed)
                    mouseComponent.DraggingFrom ??= mouseComponent.MousePosition;
                else
                    mouseComponent.DraggingFrom = null;

                if (_previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
                {
                    mouseComponent.Clicks.Enqueue(new MouseClick(mouseComponent.MousePosition, MouseButton.Left));
                }

                if (_previousMouseState.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed)
                {
                    mouseComponent.Clicks.Enqueue(new MouseClick(mouseComponent.MousePosition, MouseButton.Right));
                }
            }
            else
            {
                mouseComponent.Hovered = false;
            }
        }
        _previousMouseState = mouseState;
    }
}
