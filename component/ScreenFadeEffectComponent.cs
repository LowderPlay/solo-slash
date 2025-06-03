using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace solo_slasher.component;

public class ScreenFadeEffectComponent(double inMs, double pause, double outMs) : IComponent
{
    public TimeSpan StartTime;
    public readonly double InMs = inMs;
    public readonly double PauseMs = pause;
    public readonly double OutMs = outMs;
    public Color Color = Color.Black;
    
    public double TotalTime => InMs + PauseMs + OutMs;
}
