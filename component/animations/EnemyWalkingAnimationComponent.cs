using System;

namespace solo_slasher.component.animations;

public class EnemyWalkingAnimationComponent(TimeSpan startedAt, int frameCount) : AnimationComponent(startedAt, 150, frameCount);
