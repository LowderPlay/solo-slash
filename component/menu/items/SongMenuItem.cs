using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rhythm_cs2;
using solo_slasher.track;

namespace solo_slasher.component.menu.items;

public class SongMenuItem(Action<MouseButton, Vector2> click, Track track, Action<bool> hover, bool hovered) : HoverableMenuItem(click, hover, hovered)
{
    public override int Height => 70;
    public override void Render(SpriteBatch spriteBatch, Vector2 position, int width)
    {
        base.Render(spriteBatch, position, width);
        IMenuItem.DrawBorder(spriteBatch, position, width, Height);
        
        spriteBatch.Draw(track.Cover, new Rectangle(position.ToPoint(), new Point(Height)), Color.White);
        var artistSize = new Vector2(0, Assets.MenuFont.MeasureString(track.BeatMap.Artist).Y / 2);
        spriteBatch.DrawString(Assets.MenuFont, track.BeatMap.Artist, position + new Vector2(Height + 5, Height / 2) - artistSize * 2, Color.White);
        spriteBatch.DrawString(Assets.TitleFont, track.BeatMap.Song, position + new Vector2(Height + 5, Height / 2), Color.White);
    }
}
