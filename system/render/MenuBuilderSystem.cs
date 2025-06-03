using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.system.render;

public class MenuBuilderSystem
{
    public void Update()
    {
        foreach (var entity in EntityManager.GetEntitiesWith<UiStructureComponent>())
        {
            List<Action<SpriteBatch>> renderActions = [];
            var controller = EntityManager.GetComponent<UiStructureComponent>(entity).Controller;
            var height = 0;
            foreach (var menuItem in controller.GetMenuItems())
            {
                var position = new Vector2(0, height);
                renderActions.Add(sb =>
                {
                    menuItem.Render(sb, position, controller.Width);
                });
                if (EntityManager.TryGetComponent<MouseControllableComponent>(entity, out var mouseControllable))
                {
                    var topLeft = position + new Vector2(0, mouseControllable.Scroll);
                    var bottomRight = topLeft + new Vector2(controller.Width, menuItem.Height);
                    bool InBounds(Vector2 position) => position.X > topLeft.X 
                                                       && position.Y > topLeft.Y 
                                                       && position.X < bottomRight.X 
                                                       && position.Y < bottomRight.Y;
                    menuItem.OnHover(mouseControllable.Hovered && InBounds(mouseControllable.MousePosition));
                    if (mouseControllable.Clicks.TryPeek(out var click) && InBounds(click.Position))
                    {
                        mouseControllable.Clicks.Dequeue();
                        menuItem.OnClick(click.MouseButton, click.Position - topLeft);
                    }

                    if (mouseControllable.DraggingFrom != null && InBounds(mouseControllable.DraggingFrom.Value)) 
                        menuItem.OnHold(mouseControllable.MousePosition);
                }
                height += menuItem.Height;
            }

            if (EntityManager.TryGetComponent<MouseControllableComponent>(entity, out var mouse))
            {
                mouse.Clicks.Clear();
            }


            if (!EntityManager.TryGetComponent<InternalCanvasComponent>(entity, out var canvas))
            {
                canvas = new InternalCanvasComponent();
                EntityManager.AddComponent(entity, canvas);
            }

            canvas.InternalRender = (sb, _) =>
            {
                foreach (var action in renderActions)
                {
                    action.Invoke(sb);
                }
            };
            canvas.Size = new Point(controller.Width, height);
        }
    }
}
