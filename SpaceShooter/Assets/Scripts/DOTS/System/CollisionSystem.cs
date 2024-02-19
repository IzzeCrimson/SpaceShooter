using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct CollisionSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var projectile in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<ProjectileTag>())
        {
            foreach (var asteroid in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<AsteroidTag>().WithEntityAccess())
            {
                if (math.distancesq(projectile.ValueRO.Position, asteroid.Item1.ValueRO.Position) < 0.5f)
                {
                    SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).DestroyEntity(asteroid.Item2);
                }
            }
        }
    }
}
