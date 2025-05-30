using System;
using solo_slasher.duel;

namespace solo_slasher.component;

public class PlayingTrackComponent : IComponent
{
    public TrackState TrackState;
    public TimeSpan? StartTime = null;
}
