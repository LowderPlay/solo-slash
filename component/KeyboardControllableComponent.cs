using Microsoft.Xna.Framework.Input;

namespace solo_slasher.component;

public class KeyboardControllableComponent : IComponent
{
    public Keys UpKey = Keys.W;
    public Keys LeftKey = Keys.A;
    public Keys DownKey = Keys.S;
    public Keys RightKey = Keys.D;
    public float StepsPerSecond;
}