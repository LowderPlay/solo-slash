namespace solo_slasher.component;

public class HealthComponent : IComponent
{
    public int Health = 100;

    public void AddHealth(int amount)
    {
        if (Health + amount > 100) Health = 100;
        else if (Health + amount < 0) Health = 0;
        else Health += amount;
    }
}