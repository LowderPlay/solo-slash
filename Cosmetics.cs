using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.config;

namespace solo_slasher;

public enum CosmeticType
{
    Guitar,
    Shirt,
    Pants
}

public class CosmeticItem
{
    public CosmeticType Type;
    public Texture2D Texture;
    public string Name;
    public int Price;

    public bool Hovered;
    public bool Selected =>
        ConfigManager.Config.PickedGuitar == Texture.Name 
        || ConfigManager.Config.PickedShirt == Texture.Name 
        || ConfigManager.Config.PickedPants == Texture.Name;

    public void Select()
    {
        if (!ConfigManager.Config.AcquiredItems.Contains(Texture.Name))
        {
            if (ConfigManager.Config.CoinBalance >= Price)
            {
                ConfigManager.Config.CoinBalance -= Price;
                ConfigManager.Config.AcquiredItems.Add(Texture.Name);
                Assets.CoinEffect.Play(ConfigManager.Config.SoundVolume, 0, 0);
            }
            else
            {
                Assets.Click.Play(ConfigManager.Config.SoundVolume, 0, 0);
                return;
            }
        }
        
        switch (Type)
        {
            case CosmeticType.Guitar:
                ConfigManager.Config.PickedGuitar = Texture.Name;
                break;
            case CosmeticType.Shirt:
                ConfigManager.Config.PickedShirt = Texture.Name;
                break;
            case CosmeticType.Pants:
                ConfigManager.Config.PickedPants = Texture.Name;
                break;
        }
    }
}

public static class Cosmetics
{
    public static readonly Dictionary<string, CosmeticItem> Shirts =
        new()
        {
            [Assets.PlayerShirtBlack.Name] = new CosmeticItem
            {
                Type = CosmeticType.Shirt,
                Name = "Чёрная",
                Texture = Assets.PlayerShirtBlack,
            },
            [Assets.PlayerShirtGreen.Name] = new CosmeticItem
            {
                Type = CosmeticType.Shirt,
                Name = "Зелёная",
                Texture = Assets.PlayerShirtGreen,
                Price = 10,
            },
            [Assets.PlayerShirtPink.Name] = new CosmeticItem
            {
                Type = CosmeticType.Shirt,
                Name = "Розовая",
                Texture = Assets.PlayerShirtPink,
                Price = 100,
            },
            [Assets.PlayerShirtWhite.Name] = new CosmeticItem
            {
                Type = CosmeticType.Shirt,
                Name = "Белая",
                Texture = Assets.PlayerShirtWhite,
                Price = 300,
            },
        };

    public static readonly Dictionary<string, CosmeticItem> Pants = 
        new() 
        {
            [Assets.PlayerPantsGray.Name] = new CosmeticItem 
            {
                Type = CosmeticType.Pants,
                Name = "Серые",
                Texture = Assets.PlayerPantsGray,
            },
            [Assets.PlayerPantsWhite.Name] = new CosmeticItem 
            {
                Type = CosmeticType.Pants,
                Name = "Белые",
                Texture = Assets.PlayerPantsWhite,
                Price = 500,
            }, 
        };

    public static readonly Dictionary<string, CosmeticItem> Guitars = 
        new()
        {
            [Assets.PlayerGuitar1.Name] = new CosmeticItem 
            { 
                Type = CosmeticType.Guitar,
                Name = "Squier",
                Texture = Assets.PlayerGuitar1,
            },
            [Assets.PlayerGuitar2.Name] = new CosmeticItem {
                Type = CosmeticType.Guitar,
                Name = "Strandberg",
                Texture = Assets.PlayerGuitar2,
                Price = 500,
            },
            [Assets.PlayerGuitar3.Name] = new CosmeticItem {
                Type = CosmeticType.Guitar,
                Name = "Stratocaster",
                Texture = Assets.PlayerGuitar3,
                Price = 700,
            },
        };
}
