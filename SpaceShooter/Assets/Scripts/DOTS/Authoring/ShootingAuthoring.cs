using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShootingAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float FireRate;
    public Transform SpawnPosition;

}

class ShooterBaker : Baker<ShootingAuthoring>
{
    public override void Bake(ShootingAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new ShootingData
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            FireRate = authoring.FireRate,
            SpawnPosition = authoring.SpawnPosition.position,
            Cooldown = 0

        });
    }
}
