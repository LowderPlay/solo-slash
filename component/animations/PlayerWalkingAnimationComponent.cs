using System;

namespace solo_slasher.component.animations;

public class PlayerWalkingAnimationComponent(TimeSpan startedAt) : AnimationComponent(startedAt, 150, 4);
