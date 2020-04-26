using Unity.Entities;

[GenerateAuthoringComponent]
public struct FoodEntityComponent : IComponentData
{
    public Entity foodSpotEntity;
}
