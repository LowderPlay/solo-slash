using Microsoft.Xna.Framework;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.render;

namespace solo_slasher.system;

public class UiSystem(EntityManager entityManager)
{
    private Entity duelBar;

    public void Initialize(Rectangle viewportBounds)
    {
        duelBar = entityManager.CreateEntity();
        entityManager.AddComponent(duelBar, new ScreenPositionComponent
        {
            Position = Vector2.Zero,
        });
        var scale = (float)viewportBounds.Width / AssetsManager.DuelBar.Width;
        entityManager.AddComponent(duelBar, new SizeComponent
        {
            Size = new Vector2(viewportBounds.Width, scale * AssetsManager.DuelBar.Height),
        });
        entityManager.AddComponent(duelBar, new TextureComponent
        {
            Texture = AssetsManager.DuelBar,
        });
        entityManager.AddComponent(duelBar, new HiddenComponent());
        entityManager.AddComponent(duelBar, new ZOrderComponent { ZOrder = 2 });
    }
    
    public void Update(GameTime gameTime)
    {
        if (!entityManager.TryGetFirstEntityWith<KeyboardControllableComponent>(out var player)) return;

        if (entityManager.HasComponent<DuelingComponent>(player))
        {
            entityManager.RemoveComponent<HiddenComponent>(duelBar);
        }
        else
        {
            entityManager.AddComponent(duelBar, new HiddenComponent());
        }
    }
}