using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class WrapToMapSystem : SystemBase {

    private float mapRadius;
    private float mapHeight;
    private World defaultWorld;
    private EntityManager entityManager;

    protected override void OnStartRunning() {

        // get world and entity manager to keep lines short
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        // relies on the fact that there is only one mapEntity
        // and only one entity with a MapData component
        Entity mapEntity = GetEntityQuery(typeof(MapData)).GetSingletonEntity();
        mapRadius = entityManager.GetComponentData<MapData>(mapEntity).mapRadius;
        mapHeight = entityManager.GetComponentData<MapData>(mapEntity).mapHeight;
    }
    protected override void OnUpdate() {
        float maxHeight = mapHeight;
        float maxDistance = mapRadius;
        Entities.ForEach((ref Translation translation) => {
            if (translation.Value.y > maxHeight) {
                translation.Value.y = maxHeight;
            }

            float2 entityLocation = new float2(translation.Value.x, translation.Value.z);
            float distanceFromCenter = distance(entityLocation, new float2(0f, 0f));
            if (distanceFromCenter > maxDistance) {
                float2 newLocation = entityLocation * (maxDistance / distanceFromCenter);
                translation.Value.x = -newLocation.x;
                translation.Value.z = -newLocation.y;
            }
        }).ScheduleParallel();
    }
}