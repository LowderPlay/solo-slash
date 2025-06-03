using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.component;
using solo_slasher.component.menu;
using solo_slasher.component.menu.items;
using solo_slasher.component.render;

namespace solo_slasher.prefabs.menus;


internal class ShopController : IMenuController
{
    public int Width => ShopMenuPrefab.ScreenSize.X;
    public int Height => ((IMenuController)this).MeasureHeight();
    public IEnumerable<IMenuItem> GetMenuItems()
    {
        List<(string name, IEnumerable<CosmeticItem> items)> categories = [
            ("Футболки", Cosmetics.Shirts.Values),        
            ("Штаны", Cosmetics.Pants.Values),        
            ("Гитары", Cosmetics.Guitars.Values),        
        ];
        foreach (var (name, items) in categories)
        {
            yield return new HeadingMenuItem(name);
            foreach (var item in items)
            {
                yield return new ShopMenuItem(item);
            }
        }
    }
}

public static class ShopMenuPrefab
{
    private static readonly Point ScreenPosition = new Point(32, 58) * new Point(2);
    internal static readonly Point ScreenSize = new Point(101, 83) * new Point(2);
    private static Rectangle _screenRect;
    private static readonly Vector2 Alignment = new(0.5f, 0.9f);
    
    public static void Create()
    {
        var offset = new Vector2(Assets.Shop.Width, Assets.Shop.Height) * Alignment * 2;
        _screenRect = new Rectangle((-offset).ToPoint() + ScreenPosition, ScreenSize);
        
        var shop = EntityManager.CreateEntity();
        var controller = new ShopController();
        EntityManager.AddComponent(shop, new PositionComponent { Position = new Vector2(-300, -50) });
        EntityManager.AddComponent(shop, new UiStructureComponent
        {
            Controller = controller
        });
        EntityManager.AddComponent(shop, new MouseControllableComponent
        {
            HitBox = _screenRect,
            ScrollRange = (Math.Min(-controller.Height + ScreenSize.Y, 0), 0),
            ScrollScale = 32,
        });
        EntityManager.AddComponent(shop, new RenderPipelineComponent(RenderPipeline));
    }

    private static IEnumerable<IRenderOperation> RenderPipeline(GameTime gameTime, Entity entity)
    {
        var mouseControllable = EntityManager.GetComponent<MouseControllableComponent>(entity);
        
        yield return new TextureOperation
        {
            Alignment = Alignment,
            Sheet = Assets.Shop,
            InternalScale = 2f,
        };
        
        yield return new ScrollableInternalCanvasOperation(-mouseControllable.Scroll, _screenRect);
    }
}
