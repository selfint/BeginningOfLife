using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpawnerSystem : ComponentSystem {

    private float spwanTimer;
    public Random random;
    protected override void OnCreate() {
        random = new Random(56);
    }
    protected override void OnUpdate() {
        spwanTimer -= Time.DeltaTime;

        if (spwanTimer <= 0f) {
            spwanTimer = .5f;

            Entity spawnedEntity = EntityManager.Instantiate(PrefabConverter.soupSpawnerEntity);
            EntityManager.SetComponentData(spawnedEntity,
                new Translation {
                    Value = new float3(random.NextFloat(-5f, 5f), random.NextFloat(-5f, 5f), 0)
                }
            );
        }
    }

}
