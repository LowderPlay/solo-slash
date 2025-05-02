using System.Collections.Generic;

namespace solo_slasher;

public class PerformanceTracker
{
    private const int windowSize = 100;
    
    private Queue<double> drawTimes = new();
    private double _drawTotal;
    private Queue<double> updateTimes = new();
    private double _updateTotal;
    
    public void SetDrawDelta(double drawDelta)
    {
        drawTimes.Enqueue(drawDelta);
        _drawTotal += drawDelta;
        if (drawTimes.Count >= windowSize)
        {
            _drawTotal -= drawTimes.Dequeue();
        }
    }

    public void SetUpdateDelta(double updateDelta)
    {
        updateTimes.Enqueue(updateDelta);
        _updateTotal += updateDelta;
        if (updateTimes.Count >= windowSize)
        {
            _updateTotal -= updateTimes.Dequeue();
        }
    }

    public double UpdatesPerSecond => _updateTotal == 0 ? 0 : updateTimes.Count / _updateTotal;
    public double FramesPerSecond => _drawTotal == 0 ? 0 : drawTimes.Count / _drawTotal;
}