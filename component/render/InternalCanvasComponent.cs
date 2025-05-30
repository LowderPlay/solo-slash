using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace solo_slasher.component.render;

public class InternalCanvasComponent : IComponent
{
    public Point Size;
    public Action<SpriteBatch, Entity> InternalRender;
    public RenderTarget2D Target;
}
