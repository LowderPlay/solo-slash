using System.Collections.Generic;
using System.Linq;
using solo_slasher.component.menu.items;

namespace solo_slasher.component.menu;

public interface IMenuController
{
    public int Width { get; }
    public IEnumerable<IMenuItem> GetMenuItems();

    public int MeasureHeight()
    {
        return GetMenuItems().Select(i => i.Height).Sum();
    }
}
