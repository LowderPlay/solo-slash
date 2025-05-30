using System;

namespace solo_slasher.component;

public class EnemyAiComponent : IComponent
{
     public PerlinNoise Noise;
     public TimeSpan LastHit = TimeSpan.Zero;
     public float StayDistance;
     public float Velocity = 150;
}