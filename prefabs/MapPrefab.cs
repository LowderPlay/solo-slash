using System;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.component.render.pipelines;

namespace solo_slasher.prefabs;

public static class MapPrefab
{
    // public static void Create() {
    //     var background = EntityManager.CreateEntity();
    //     EntityManager.AddComponent(background, new ScaleComponent { Scale = 4f });
    //     EntityManager.AddComponent(background, new PositionComponent { Position = Vector2.Zero });
    //     EntityManager.AddComponent(background, SimplePipelineBuilder.Build(new TextureOperation
    //     {
    //         Texture = Assets.Background,
    //         Alignment = new Vector2(0.5f, 0.5f),
    //     }));
    // }
    private static readonly Random Random = new (); 

    public static void GenerateChunk(Vector2 chunkPosition)
    {
        if (Random.NextDouble() <= 0.7) return;
        CreateGrass(chunkPosition, Random.Next(0, 16));
    }

    private static void CreateGrass(Vector2 chunkPosition, int type)
    {
        var grass = EntityManager.CreateEntity();
        EntityManager.AddComponent(grass, new ScaleComponent { Scale = 2f });
        EntityManager.AddComponent(grass, new PositionComponent
        {
            Position = chunkPosition
        });
        EntityManager.AddComponent(grass, new SheetIndexComponent
        {
            X = type % 4,
            Y = type / 4,
        });
        EntityManager.AddComponent(grass, SimplePipelineBuilder.Build(new SpritesheetOperation
        {
            Alignment = new Vector2(0.5f, 0.6f),
            Sheet = Assets.Grass,
            Size = (4, 4)
        }));
    }
}