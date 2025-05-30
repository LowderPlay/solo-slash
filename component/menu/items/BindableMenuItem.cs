using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.menu.items;

public abstract class BindableMenuItem(Action<MouseButton, Vector2> click, Action<bool> hover, Action<Vector2> hold) : IMenuItem
{
    public abstract int Height { get; }
    public abstract void Render(SpriteBatch spriteBatch, Vector2 position, int width);

    public virtual void OnClick(MouseButton button, Vector2 position) => click.Invoke(button, position);
    public virtual void OnHold(Vector2 position) => hold.Invoke(position);
    public virtual void OnHover(bool isOver) => hover.Invoke(isOver);
}
