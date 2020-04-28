using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Random = UnityEngine.Random;

public class WorldBuilder : MonoBehaviour {
    [SerializeField] public float mapRadius;
    [SerializeField] public int foodSpawnerAmount;
    [SerializeField] public int randomSeed;
    [SerializeField] public PrefabConverter prefabConverter;

    private World defaultWorld;
    private EntityManager entityManager;
    void Start() {
        Random.InitState(randomSeed);
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        for (int i = 0; i < foodSpawnerAmount; i++) {
            Entity newFoodSpawner = entityManager.Instantiate(prefabConverter.foodSpawnerEntityPrefab);
            Vector2 randomDirection = Random.insideUnitCircle * mapRadius;
            float3 randomPoint = new float3(randomDirection.x, 0f, randomDirection.y);
            entityManager.SetComponentData(newFoodSpawner, new Translation {
                Value = randomPoint
            });
        }
    }
}
