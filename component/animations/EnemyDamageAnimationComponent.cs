using System;

namespace solo_slasher.component.animations;

public class EnemyDamageAnimationComponent(TimeSpan startedAt) : AnimationComponent(startedAt, 100, 4)
{
    public readonly TimeSpan EndTime = startedAt + TimeSpan.FromMilliseconds(100 * 4);
}
