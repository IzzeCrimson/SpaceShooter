using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct CharacterMovementSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }
    
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;

        new CharacterMoveJob()
        {
            DeltaTime = deltaTime

        }.Schedule();
    }

    [BurstCompile]
    public partial struct CharacterMoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ref LocalTransform transform, in InputsData inputsData, in CharacterData characterData)
        {
            var input = inputsData;

            input.move.x = input.move.y > -0.99f ? -input.move.x : input.move.x;

            transform.Position += transform.Up() * input.move.y * (characterData.movementSpeed * DeltaTime);
            transform.Rotation = math.mul(transform.Rotation, quaternion.RotateZ(input.move.x * (characterData.rotationSpeed * DeltaTime)));

        }
    }
}
