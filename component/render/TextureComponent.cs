using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class TextureComponent : IComponent
{
    public Texture2D Texture;
    
    public Vector2 Alignment = new(0f, 0f);
}