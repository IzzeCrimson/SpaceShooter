using Unity.Entities;
using Unity.Mathematics;

public struct AsteroidTag : IComponentData
{
    public float3 Direction;
    public float Speed;
    public Entity Self;
    public int SortKey;
}
