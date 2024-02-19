using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float Speed;
}


public class ProjectileBaker : Baker<ProjectileAuthoring>
{

    public override void Bake(ProjectileAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        //AddComponent(entity, new ProjectileTag
        //{
        //    Speed = authoring.Speed
        //});
    }
}
