using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct ShootingData : IComponentData
{
    public Entity Prefab;
    public float3 SpawnPosition;
    public float Cooldown;
    public float FireRate;
}
