using System;
using Microsoft.Xna.Framework.Audio;
using solo_slasher.duel;

namespace solo_slasher.component;

public class PlayingTrackComponent : IComponent
{
    public TrackState TrackState;
    public TimeSpan? StartTime = null;
    public SoundEffectInstance SoundEffect;
    public TimeSpan Duration;
    public TimeSpan BeforePlaying;
}
