using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.prefabs;

namespace solo_slasher.system;

public class MapFillerSystem
{
    private readonly Vector2 chunkSize = new (64, 64); 
    private readonly Vector2 fillChunks = new (15, 10);
    
    private HashSet<Point> filledChunks = [];
    
    public void Update()
    {
        if(!EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var camera)) return;
        var position = EntityManager.GetComponent<PositionComponent>(camera);
        var fov = new Rectangle(
            (position.Position / chunkSize - fillChunks).ToPoint(), 
            (fillChunks * 2).ToPoint()
            );
        
        for (var x = fov.Left; x < fov.Right; x++)
        {
            for (var y = fov.Top; y < fov.Bottom; y++)
            {
                var chunk = new Point(x, y);
                if (filledChunks.Contains(chunk)) continue;
                MapPrefab.GenerateChunk(chunk.ToVector2() * chunkSize);
                filledChunks.Add(chunk);
            }
        }
    }
}