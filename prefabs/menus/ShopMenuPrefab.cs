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

internal enum ItemType
{
    Guitar,
    Shirt,
    Pants
}

internal class ShopItem
{
    public ItemType Type;
    public Texture2D Texture;
    public string Name;

    public bool Hovered;
    public bool Selected
    {
        get
        {
            var cosmetics = EntityManager.TryGetFirstEntityWith<PlayerCosmeticsComponent>(out var player) ? 
                EntityManager.GetComponent<PlayerCosmeticsComponent>(player) : 
                new PlayerCosmeticsComponent();
            return cosmetics.Pants == Texture || cosmetics.Shirt == Texture || cosmetics.Guitar == Texture;
        }
    }

    public void Select()
    {
        var cosmetics = EntityManager.TryGetFirstEntityWith<PlayerCosmeticsComponent>(out var player) ? 
            EntityManager.GetComponent<PlayerCosmeticsComponent>(player) : 
            new PlayerCosmeticsComponent();
        
        switch (Type)
        {
            case ItemType.Guitar:
                cosmetics.Guitar = Texture;
                break;
            case ItemType.Shirt:
                cosmetics.Shirt = Texture;
                break;
            case ItemType.Pants:
                cosmetics.Pants = Texture;
                break;
        }
    }
}

internal class ShopController : IMenuController
{
    public int Width => ShopMenuPrefab.ScreenSize.X;
    public int Height => ((IMenuController)this).MeasureHeight();
    public IEnumerable<IMenuItem> GetMenuItems()
    {
        List<(string name, IEnumerable<ShopItem> items)> categories = [
            ("Футболки", ShopMenuPrefab.Shirts),        
            ("Штаны", ShopMenuPrefab.Pants),        
            ("Гитары", ShopMenuPrefab.Guitars),        
        ];
        foreach (var (name, items) in categories)
        {
            yield return new HeadingMenuItem(name);
            foreach (var item in items)
            {
                yield return new SelectableSpriteMenuItem(
                    (_, _) => item.Select(), 
                    hover => item.Hovered = hover,
                    item.Selected, 
                    item.Hovered, 
                    new SpritesheetOperation
                    {
                        Sheet = item.Texture,
                        Alignment = new Vector2(0.5f, 0.5f),
                        Size = (6, 2)
                    }, new Vector2(3, 0), item.Name);
            }
        }
    }
}

public static class ShopMenuPrefab
{
    internal static readonly ShopItem[] Shirts =
    [
        new()
        {
            Type = ItemType.Shirt,
            Name = "Чёрная",
            Texture = Assets.PlayerShirtBlack,
        },
        new()
        {
            Type = ItemType.Shirt,
            Name = "Зелёная",
            Texture = Assets.PlayerShirtGreen,
        },
        new()
        {
            Type = ItemType.Shirt,
            Name = "Розовая",
            Texture = Assets.PlayerShirtPink,
        },
        new()
        {
            Type = ItemType.Shirt,
            Name = "Белая",
            Texture = Assets.PlayerShirtWhite,
        },
    ];
    internal static readonly ShopItem[] Pants = [
        new() {
            Type = ItemType.Pants,
            Name = "Серые",
            Texture = Assets.PlayerPantsGray,
        },
        new() {
            Type = ItemType.Pants,
            Name = "Белые",
            Texture = Assets.PlayerPantsWhite,
        },
    ];
    internal static readonly ShopItem[] Guitars = [
        new() {
            Type = ItemType.Guitar,
            Name = "Squier",
            Texture = Assets.PlayerGuitar1,
        },
        new() {
            Type = ItemType.Guitar,
            Name = "Strandberg",
            Texture = Assets.PlayerGuitar2,
        },
        new() {
            Type = ItemType.Guitar,
            Name = "Stratocaster",
            Texture = Assets.PlayerGuitar3,
        },
    ];
    
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
