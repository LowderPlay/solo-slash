using System;
using System.Collections.Generic;

namespace solo_slasher.duel;

public class DuelState
{
    public int Bpm = 120;
    public Queue<Note> Notes;
    public TimeSpan StartTime;
}