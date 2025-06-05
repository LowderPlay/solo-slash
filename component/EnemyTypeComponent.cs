namespace solo_slasher.component;

public enum EnemyType
{
    Mushroom,
    Beetroot,
    Pumpkin,
}

public class EnemyTypeComponent : IComponent
{
    public EnemyType EnemyType;
}
