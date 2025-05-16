using System;
using Microsoft.Xna.Framework;

namespace solo_slasher;

public class PerlinNoise
{
    private readonly int[] _permutation;

    public PerlinNoise(int seed = 0)
    {
        _permutation = new int[512];
        var p = new int[256];
        var rand = new Random(seed);

        for (var i = 0; i < 256; i++)
            p[i] = i;

        for (var i = 0; i < 256; i++)
        {
            var swapIndex = rand.Next(256);
            (p[i], p[swapIndex]) = (p[swapIndex], p[i]);
        }

        for (var i = 0; i < 512; i++)
            _permutation[i] = p[i % 256];
    }

    public float Noise(Vector2 position)
    {
        float x = position.X / 700, y = position.Y / 700;
        var xi = (int)Math.Floor(x) & 255;
        var yi = (int)Math.Floor(y) & 255;

        var xf = x - (float)Math.Floor(x);
        var yf = y - (float)Math.Floor(y);

        var u = Fade(xf);
        var v = Fade(yf);

        var aa = _permutation[_permutation[xi] + yi];
        var ab = _permutation[_permutation[xi] + yi + 1];
        var ba = _permutation[_permutation[xi + 1] + yi];
        var bb = _permutation[_permutation[xi + 1] + yi + 1];

        var x1 = Lerp(Grad(aa, xf, yf), Grad(ba, xf - 1, yf), u);
        var x2 = Lerp(Grad(ab, xf, yf - 1), Grad(bb, xf - 1, yf - 1), u);
        return (Lerp(x1, x2, v) + 1) / 2; 
    }

    private static float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    private static float Lerp(float a, float b, float t)
    {
        return a + t * (b - a);
    }

    private static float Grad(int hash, float x, float y)
    {
        return (hash & 0x3) switch
        {
            0 => x + y,
            1 => -x + y,
            2 => x - y,
            3 => -x - y,
            _ => 0
        };
    }
}