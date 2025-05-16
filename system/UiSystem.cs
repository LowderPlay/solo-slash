using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;
using solo_slasher.component.render.pipelines;

namespace solo_slasher.system;

public class UiSystem
{
    private Entity duelBar;

    public void Initialize(Rectangle viewportBounds)
    {
        duelBar = EntityManager.CreateEntity();
        EntityManager.AddComponent(duelBar, new ScreenPositionComponent
        {
            Position = Vector2.Zero,
        });
        var scale = (float)viewportBounds.Width / Assets.DuelBar.Width;
        EntityManager.AddComponent(duelBar, new SizeComponent
        {
            Size = new Vector2(viewportBounds.Width, scale * Assets.DuelBar.Height),
        });
        EntityManager.AddComponent(duelBar, SimplePipelineBuilder.Build(new TextureOperation
        {
            Texture = Assets.DuelBar,
        }));
        EntityManager.AddComponent(duelBar, new HiddenComponent());
        EntityManager.AddComponent(duelBar, new ZOrderComponent { ZOrder = 2 });
    }
    
    public void Update(GameTime gameTime)
    {
        if (!EntityManager.TryGetFirstEntityWith<KeyboardControllableComponent>(out var player)) return;

        if (EntityManager.HasComponent<PlayingTrackComponent>(player))
        {
            EntityManager.RemoveComponent<HiddenComponent>(duelBar);
        }
        else
        {
            EntityManager.AddComponent(duelBar, new HiddenComponent());
        }
    }
}