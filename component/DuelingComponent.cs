using solo_slasher.duel;

namespace solo_slasher.component;

public class DuelingComponent : IComponent
{
    public Entity DuelWith;
    public DuelState DuelState;
}