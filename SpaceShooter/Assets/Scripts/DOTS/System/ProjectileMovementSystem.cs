using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct ProjectileMovementSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        
    }


    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);

        var deltaTime = SystemAPI.Time.DeltaTime;

        new ProjectileMoveJob()
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
    public partial struct ProjectileMoveJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter Ecb;

        [BurstCompile]
        private void Execute(ref LocalTransform transform, in ProjectileTag projectileTag)
        {
            if (transform.Position.x < -18f || transform.Position.x > 18f || transform.Position.y < -10f || transform.Position.y > 10f)
            {
                Ecb.DestroyEntity(projectileTag.SortKey, projectileTag.Self);
            }
            else
            {
                var newPos = transform.Position + transform.Up() * (projectileTag.Speed * DeltaTime);

                transform.Position = newPos;
                
            }

        }
    }
}
