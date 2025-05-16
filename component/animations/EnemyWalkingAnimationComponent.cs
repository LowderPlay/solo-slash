using System;

namespace solo_slasher.component.animations;

public class EnemyWalkingAnimationComponent(TimeSpan startedAt) : AnimationComponent(startedAt, 150, 4);