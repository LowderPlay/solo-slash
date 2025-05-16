using System.Collections.Generic;

namespace solo_slasher;

public class PerformanceTracker
{
    private const int SampleWindowSize = 30;
    
    private readonly Queue<double> _drawTimes = new();
    private double _drawTotal;
    private readonly Queue<double> _updateTimes = new();
    private double _updateTotal;
    
    public void SetDrawDelta(double drawDelta)
    {
        _drawTimes.Enqueue(drawDelta);
        _drawTotal += drawDelta;
        while (_drawTimes.Count > SampleWindowSize)
        {
            _drawTotal -= _drawTimes.Dequeue();
        }
    }

    public void SetUpdateDelta(double updateDelta)
    {
        _updateTimes.Enqueue(updateDelta);
        _updateTotal += updateDelta;
        while (_updateTimes.Count > SampleWindowSize)
        {
            _updateTotal -= _updateTimes.Dequeue();
        }
    }

    public double UpdatesPerSecond => _updateTotal == 0 ? 0 : _updateTimes.Count / _updateTotal;
    public double FramesPerSecond => _drawTotal == 0 ? 0 : _drawTimes.Count / _drawTotal;
}