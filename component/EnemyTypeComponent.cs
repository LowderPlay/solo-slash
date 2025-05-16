namespace solo_slasher.component;

public enum EnemyType
{
    Mushroom,
    Beetroot,
}

public class EnemyTypeComponent : IComponent
{
    public EnemyType EnemyType;
}