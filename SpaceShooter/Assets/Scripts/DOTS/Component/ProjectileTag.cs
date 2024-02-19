using Unity.Entities;

public struct ProjectileTag : IComponentData
{
    public float Speed;
    public Entity Self;
    public int SortKey;
}
