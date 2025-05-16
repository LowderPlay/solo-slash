using System;
using Microsoft.Xna.Framework;

namespace solo_slasher.component.animations;

public abstract class AnimationComponent(TimeSpan startedAt, int frameInterval, int frameCount) : IComponent
{
    public int GetCurrentFrame(GameTime now)
    {
        return ((int)(now.TotalGameTime - startedAt).TotalMilliseconds / frameInterval + 1) % frameCount;
    }
}