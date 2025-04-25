using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class TextComponent : IComponent
{
    public SpriteFont Font;
    public string Text;

    public Vector2 Alignment = new(0.5f, 0.5f);
}