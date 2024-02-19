using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct AsteroidMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);

        var deltaTime = SystemAPI.Time.DeltaTime;

        new AsteroidMoveJob()
        {
            DeltaTime = deltaTime,
            Ecb = ecb

        }.Schedule();
    }

    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        return ecb.AsParallelWriter();
    }

    public void OnDestroy()
    {

    }

    [BurstCompile]
    public partial struct AsteroidMoveJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter Ecb;

        [BurstCompile]
        private void Execute(ref LocalTransform transform, in AsteroidTag asteroidTag)
        {

            if (transform.Position.x < -25f || transform.Position.x > 25f || transform.Position.y < -15f || transform.Position.y > 15f)
            {
                Ecb.DestroyEntity(asteroidTag.SortKey, asteroidTag.Self);
            }
            else
            {
                var newPos = transform.Position + (asteroidTag.Direction * asteroidTag.Speed * DeltaTime);

                transform.Position = newPos;
                
            }
        }
    }
}
