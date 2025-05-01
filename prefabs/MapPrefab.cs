using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.component.render.pipelines;

namespace solo_slasher.prefabs;

public static class MapPrefab
{
    public static void Create() {
        var background = EntityManager.CreateEntity();
        EntityManager.AddComponent(background, new ScaleComponent { Scale = 4f });
        EntityManager.AddComponent(background, new PositionComponent { Position = Vector2.Zero });
        EntityManager.AddComponent(background, SimplePipelineBuilder.Build(new TextureOperation
        {
            Texture = Assets.Background,
            Alignment = new Vector2(0.5f, 0.5f),
        }));
    }
}