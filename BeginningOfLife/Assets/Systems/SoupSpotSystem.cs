using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

public class SoupSpotSystem : ComponentSystem {

    public float mapRadius = 20f;
    public int soupSpotAmount = 20;
    public int spawnFoodRate = 1000;  // in *FRAMES*
    public int maxFoodAmount = 200;
    private int spawnFoodTimer;

    /// <summary>
    /// Spawn initial soup spots in random location on the map.
    /// </summary>
    private void spawnSoupSpots() {
        for (int i = 0; i < soupSpotAmount; i++) {
            Entity spawnedEntity = EntityManager.Instantiate(PrefabConverter.soupSpawnerEntity);
            Vector2 xz = Random.insideUnitCircle * mapRadius;
            float y = 0;  // TODO: set this to the height of the map at xz
            float3 position = new float3(xz.x, y, xz.y);

            EntityManager.SetComponentData(spawnedEntity, new Translation { Value = position });
        }
    }

    /// <summary>
    /// Spawn foods from every soup spot periodically
    /// </summary>
    private void spawnFoods() {

        spawnFoodTimer += 1;

        if (spawnFoodTimer  >= spawnFoodRate) {
            // TODO: spawn food
        }
    }
    protected override void OnCreate() { }
    protected override void OnStartRunning() {
        spawnSoupSpots();
    }
    protected override void OnUpdate() {
        spawnFoods();
    }
}
