using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DemoAuthorScript : MonoBehaviour
{

    public float value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct DemoFloat : IComponentData
{
    public float Value;
}

public class DemoBaker : Baker<DemoAuthorScript>
{
    public override void Bake(DemoAuthorScript authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

    }

}
