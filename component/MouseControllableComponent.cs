using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace solo_slasher.component;

public enum MouseButton
{
    Left,
    Right,
}

public struct MouseClick(Vector2 position, MouseButton mouseButton)
{
    public readonly Vector2 Position = position;
    public readonly MouseButton MouseButton = mouseButton;
}

public class MouseControllableComponent : IComponent
{
    // Relative to entity position (without scale)
    public Rectangle HitBox;
    public Vector2 MousePosition = Vector2.Zero;
    public bool Hovered = false;
    public Vector2? DraggingFrom = null;
    public Queue<MouseClick> Clicks = new();
    
    public int Scroll = 0;
    public int ScrollScale = 1;
    public (int min, int max) ScrollRange;
}
