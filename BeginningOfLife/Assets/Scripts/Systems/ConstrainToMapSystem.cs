using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class ConstrainToMapSystem : SystemBase {

    private float mapRadius;
    private float mapHeight;
    private World defaultWorld;
    private EntityManager entityManager;

    protected override void OnStartRunning() {

        // get world and entity manager to keep lines short
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        Entity mapEntity = GetEntityQuery(typeof(MapData)).GetSingletonEntity();
        mapRadius = entityManager.GetComponentData<MapData>(mapEntity).mapRadius;
        mapHeight = entityManager.GetComponentData<MapData>(mapEntity).mapHeight;
    }
    protected override void OnUpdate() {
        float maxHeight = mapHeight;
        Entities.ForEach((ref Translation translation) => {
            if (translation.Value.y > maxHeight) {
                translation.Value.y = maxHeight;
            }
        }).ScheduleParallel();
    }
}