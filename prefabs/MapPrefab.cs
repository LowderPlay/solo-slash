using System;
using System.Linq;
using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.component.render.pipelines;

namespace solo_slasher.prefabs;

public static class MapPrefab
{
    private static readonly Random Random = new (); 

    public static void GenerateChunk(Vector2 chunkPosition)
    {
        if (Random.NextDouble() <= 0.6) return;
        switch (PickRandom(15, 3, 10, 1, 5, 1))
        {
            case 0: 
                CreateItem(chunkPosition, Random.Next(0, 3), Random.Next(0, 4), new SpritesheetOperation
                {
                    Alignment = new Vector2(0.5f, 0.8f),
                    Sheet = Assets.Grass,
                    Size = (3, 4)
                });
                break;
            case 1: 
                CreateItem(chunkPosition, Random.Next(0, 3), 0, new SpritesheetOperation
                {
                    Alignment = new Vector2(0.5f, 0.8f),
                    Sheet = Assets.Bushes,
                    Size = (3, 1)
                });
                break;
            case 2: 
                EntityManager.AddComponent(CreateItem(chunkPosition, Random.Next(0, 7), 0, new SpritesheetOperation
                {
                    Alignment = new Vector2(0.5f, 0.8f),
                    Sheet = Assets.Fills,
                    Size = (7, 1)
                }), new ZOrderComponent { ZOrder = -1 });
                break;
            case 3: 
                CreateItem(chunkPosition, Random.Next(0, 3), 0, new SpritesheetOperation
                {
                    Alignment = new Vector2(0.5f, 0.8f),
                    Sheet = Assets.Pumpkins,
                    Size = (3, 1)
                });
                break;
            case 4: 
                CreateItem(chunkPosition, Random.Next(0, 3), Random.Next(0, 2), new SpritesheetOperation
                {
                    Alignment = new Vector2(0.5f, 0.8f),
                    Sheet = Assets.Rocks,
                    Size = (3, 2)
                });
                break;
            case 5: 
                CreateItem(chunkPosition, Random.Next(0, 2), 0, new SpritesheetOperation
                {
                    Alignment = new Vector2(0.5f, 0.9f),
                    Sheet = Assets.Trees,
                    Size = (2, 1)
                });
                break;
        }
    }

    private static Entity CreateItem(Vector2 chunkPosition, int x, int y, SpritesheetOperation spritesheetOperation)
    {
        var slop = new Vector2(
            (float)(Constants.ChunkSize.X * (0.5 - Random.NextDouble())), 
            (float)(Constants.ChunkSize.Y * (0.5 - Random.NextDouble())));
        var entity = EntityManager.CreateEntity();
        EntityManager.AddComponent(entity, new ScaleComponent { Scale = 2f });
        EntityManager.AddComponent(entity, new PositionComponent
        {
            Position = chunkPosition + slop
        });
        EntityManager.AddComponent(entity, new SheetIndexComponent
        {
            X = x,
            Y = y,
        });
        EntityManager.AddComponent(entity, SimplePipelineBuilder.Build(spritesheetOperation));
        return entity;
    }

    private static int PickRandom(params int[] weights)
    {
        var random = Random.NextDouble();
        var total = weights.Sum();
        var sum = 0;
        for (var i = 0; i < weights.Length; i++)
        {
            var value = (double) (sum + weights[i]) / total;
            if (random <= value) return i;
            sum += weights[i];
        }
        return weights.Length - 1;
    }
}