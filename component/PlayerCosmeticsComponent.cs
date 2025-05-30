using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;

namespace solo_slasher.component;

public class PlayerCosmeticsComponent : IComponent
{
    public Texture2D Guitar = Assets.PlayerGuitar1;
    public Texture2D Shirt = Assets.PlayerShirtGreen;
    public Texture2D Pants = Assets.PlayerPantsGray;
}
