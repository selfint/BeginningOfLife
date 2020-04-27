using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

public class SoupSpotSystem : SystemBase {

    public float mapRadius = 20f;
    public int soupSpotAmount = 20;
    public int spawnFoodRate = 1000;  // in *FRAMES*
    public int maxFoodAmount = 200;
    private int spawnFoodTimer;
    private EndSimulationEntityCommandBufferSystem ecb_system;

    private void spawnSoupSpots() {
        for (int i = 0; i < soupSpotAmount; i++) {
            Entity spawnedEntity = EntityManager.Instantiate(PrefabConverter.soupSpotEntity);
            Vector2 xz = Random.insideUnitCircle * mapRadius;
            float y = 0;  // TODO: set this to the height of the map at xz
            float3 position = new float3(xz.x, y, xz.y);

            EntityManager.SetComponentData(spawnedEntity, new Translation { Value = position });
        }
    }

    protected override void OnCreate() {
        base.OnCreate();

        ecb_system = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnStartRunning() {
        spawnSoupSpots();
    }
    protected override void OnUpdate() {
        var ecb = ecb_system.CreateCommandBuffer().ToConcurrent();

        Entities.WithName("Output").ForEach((int entityInQueryIndex, in Translation position) => {
            Entity spawnedFood = ecb.Instantiate(entityInQueryIndex, PrefabConverter.foodEntity);
            ecb.SetComponent(entityInQueryIndex, spawnedFood, position);
        }).ScheduleParallel();
    }
}
