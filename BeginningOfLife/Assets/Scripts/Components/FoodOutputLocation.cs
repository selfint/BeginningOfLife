using Unity.Entities;
using Unity.Transforms;

[GenerateAuthoringComponent]
public struct FoodOutputLocation : IComponentData
{
    public Translation Value;
}
