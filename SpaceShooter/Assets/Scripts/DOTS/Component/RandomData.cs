using Unity.Entities;
using Random = Unity.Mathematics.Random;

public struct RandomData : IComponentData
{
    public Random Value;
}
