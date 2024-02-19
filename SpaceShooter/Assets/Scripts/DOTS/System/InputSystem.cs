using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[BurstCompile]
public partial class InputSystem : SystemBase
{

    private Controls inputs = null;

    protected override void OnCreate()
    {
        inputs = new Controls();
    }

    [BurstCompile]
    protected override void OnStartRunning()
    {
        inputs.Enable();
        //inputs.Player.Shoot.performed += OnCharacterShoot;
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        foreach (RefRW<InputsData> inputData in SystemAPI.Query<RefRW<InputsData>>())
        {
            inputData.ValueRW.move = inputs.Player.Move.ReadValue<Vector2>();
            inputData.ValueRW.isShooting = inputs.Player.Shoot.IsPressed();
        }
    }

    [BurstCompile]
    protected override void OnStopRunning()
    {
        inputs.Disable();
        
    }
}

