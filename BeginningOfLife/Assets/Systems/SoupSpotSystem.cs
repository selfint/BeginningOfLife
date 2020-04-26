using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

public class SoupSpotSystem : ComponentSystem {

    private float mapRadius = 20f;
    private int soupSpotAmount = 20;
    private float spwanTimer;
    private Random random;

    /// <summary>
    /// Spawn initial soup spots in random location on the map.
    /// </summary>
    protected override void OnStartRunning() {
        Random.InitState(1);

        spwanTimer -= Time.DeltaTime;

        if (spwanTimer <= 0f) {
            spwanTimer = .5f;

            for (int i = 0; i < soupSpotAmount; i++) {
                Entity spawnedEntity = EntityManager.Instantiate(PrefabConverter.soupSpawnerEntity);
                Vector2 xz = Random.insideUnitCircle * mapRadius;
                float y = 0;  // TODO: set this to the height of the map at xz
                float3 position = new float3(xz.x, y, xz.y);

                EntityManager.SetComponentData(spawnedEntity, new Translation { Value = position });
            }
        }
    }
    protected override void OnUpdate() { }

}
