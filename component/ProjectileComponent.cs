using System;
using solo_slasher.component.animations;

namespace solo_slasher.component;

public class ProjectileComponent(TimeSpan startTime, Entity target) : AnimationComponent(startTime, 100, 6)
{
    public readonly Entity Target = target;
    public float Velocity = 400f;
    public int Damage = 10;
}