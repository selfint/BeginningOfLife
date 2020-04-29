using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MapData : IComponentData
{
    public float mapRadius;
    public float mapHeight;
}
