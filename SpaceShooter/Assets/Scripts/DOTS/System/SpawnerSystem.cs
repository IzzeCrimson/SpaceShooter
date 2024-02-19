using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);
        Random random = new Random((uint)System.Environment.TickCount);

        var player = SystemAPI.GetSingletonEntity<CharacterData>();
        var playerTransform = SystemAPI.GetComponentRO<LocalTransform>(player).ValueRO;

        // Creates a new instance of the job, assigns the necessary data, and schedules the job in parallel.
        new AsteroidSpawnerJob
        {
            ElapsedTime = SystemAPI.Time.ElapsedTime,
            Ecb = ecb,
            Random = random,
            PlayerTransform = playerTransform
        }.ScheduleParallel();
    }

    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        return ecb.AsParallelWriter();
    }


}

[BurstCompile]
public partial struct AsteroidSpawnerJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter Ecb;
    public double ElapsedTime;
    public Random Random;
    public LocalTransform PlayerTransform;

    [BurstCompile]
    private void Execute([ChunkIndexInQuery] int chunkIndex, ref Spawner spawner)
    {

        if (spawner.NextSpawnTime < ElapsedTime)
        {
            for (int i = 0; i < 10; i++)
            {

                Entity newEntity = Ecb.Instantiate(chunkIndex, spawner.Prefab);
                var position = GetRandomPosition();
                Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(position));
                Ecb.AddComponent(chunkIndex, newEntity, new AsteroidTag
                {
                    Direction = PlayerTransform.Position - position,
                    Speed = 0.1f,
                    Self = newEntity,
                    SortKey = chunkIndex
                });

                spawner.NextSpawnTime = (float)ElapsedTime + spawner.SpawnRate;

            }
        }
    }

    [BurstCompile]
    private float3 GetRandomPosition()
    {
        var randomPosition = new float3(Random.NextFloat(-1f, 1f), Random.NextFloat(-0.5f, 0.5f), 0);
        randomPosition = math.normalize(randomPosition) * 19;
        return randomPosition;
    }
}