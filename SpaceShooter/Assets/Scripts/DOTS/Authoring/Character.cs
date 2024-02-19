using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float rotationSpeed = 1.0f;

    public GameObject Prefab;
    public float FireRate;
    public Transform SpawnPosition;

}

public struct CharacterData : IComponentData
{
    public float movementSpeed;
    public float rotationSpeed;
}

public class CharachterBaker : Baker<Character>
{
    public override void Bake(Character authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new CharacterData
        {
            movementSpeed = authoring.movementSpeed,
            rotationSpeed = authoring.rotationSpeed
        });

        AddComponent(entity, new InputsData
        {

        });

        AddComponent(entity, new ShootingData
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            FireRate = authoring.FireRate,
            SpawnPosition = authoring.SpawnPosition.position,
            Cooldown = 0

        });
    }
}
