using Microsoft.Xna.Framework;

namespace solo_slasher;

public static class Constants
{
    public const int LinePosition = 170;
    public const int NoteIgnoreDistance = 200;
    public const int NoteWidth = 70;
    public const float NoteFlyDuration = 3f;
    
    public static readonly Point OffscreenDistance = new (400, 500);
    public static readonly Vector2 ChunkSize = new (64, 64);

    public const int MaxEnemies = 15;
    public const int TargetingEnemies = 5;
}
