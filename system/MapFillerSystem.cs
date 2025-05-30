using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.prefabs;

namespace solo_slasher.system;

public class MapFillerSystem
{
    private readonly Vector2 _fillChunks = new (15, 15);
    
    private readonly HashSet<Point> _filledChunks = [];
    
    public void Update()
    {
        if(!EntityManager.TryGetFirstEntityWith<CameraOriginComponent>(out var camera)) return;
        var position = EntityManager.GetComponent<PositionComponent>(camera);
        var fov = new Rectangle(
            (position.Position / Constants.ChunkSize - _fillChunks).ToPoint(), 
            (_fillChunks * 2).ToPoint()
            );
        
        for (var x = fov.Left; x < fov.Right; x++)
        {
            for (var y = fov.Top; y < fov.Bottom; y++)
            {
                var chunk = new Point(x, y);
                if (_filledChunks.Contains(chunk)) continue;
                var chunkCoords = chunk.ToVector2() * Constants.ChunkSize;
                
                if (EntityManager.GetEntitiesWith<UiStructureComponent>()
                    .Select(e => EntityManager.GetComponent<PositionComponent>(e).Position)
                    .Select(p => (p - chunkCoords).Length())
                    .DefaultIfEmpty(300).Min() > 200)
                    MapPrefab.GenerateChunk(chunkCoords);
                _filledChunks.Add(chunk);
            }
        }
    }
}
