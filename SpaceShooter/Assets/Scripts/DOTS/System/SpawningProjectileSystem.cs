using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;

[BurstCompile]
public partial struct SpawningProjectileSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var player = SystemAPI.GetSingletonEntity<CharacterData>();
        var playerTransform = SystemAPI.GetComponentRO<LocalTransform>(player).ValueRO;
        var input = SystemAPI.GetSingleton<InputsData>();

        if (input.isShooting)
        {
            EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);

            // Creates a new instance of the job, assigns the necessary data, and schedules the job in parallel.
            new SpawnProjectileJob
            {
                ElapsedTime = SystemAPI.Time.ElapsedTime,
                Ecb = ecb,
                PlayerTransform = playerTransform
            }.ScheduleParallel();

        }

    }

    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        return ecb.AsParallelWriter();
    }

    public void OnDestroy(ref SystemState state)
    {


    }

    [BurstCompile]
    public partial struct SpawnProjectileJob : IJobEntity
    {

        public EntityCommandBuffer.ParallelWriter Ecb;
        public double ElapsedTime;
        public LocalTransform PlayerTransform;

        [BurstCompile]
        private void Execute([ChunkIndexInQuery] int chunkIndex, ref ShootingData shooting)
        {
            // If the next spawn time has passed.
            if (shooting.Cooldown < ElapsedTime)
            {
                // Spawns a new entity and positions it at the spawner.
                Entity newEntity = Ecb.Instantiate(chunkIndex, shooting.Prefab);
                float3 offset = PlayerTransform.Up();
                Ecb.SetComponent(chunkIndex, newEntity, new LocalTransform
                {
                    Position = PlayerTransform.Position + (offset * 0.5f),
                    Rotation = PlayerTransform.Rotation,
                    Scale = 3f
                });

                Ecb.AddComponent(chunkIndex, newEntity, new ProjectileTag
                {
                    Self = newEntity,
                    SortKey = chunkIndex,
                    Speed = 6
                });

                // Resets the next spawn time.
                shooting.Cooldown = (float)ElapsedTime + shooting.FireRate;
            }
        }
    }
}
