using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Random = UnityEngine.Random;

/// <summary>
/// This MonoBehaviour generates the world and
/// any new entities that need to be created
/// </summary>
public class WorldManager : MonoBehaviour {

    // GameObject prefabs
    [SerializeField] GameObject foodSpawnerPrefab;
    [SerializeField] GameObject foodPrefab;

    // World generate configuration
    [SerializeField] public float mapRadius;
    [SerializeField] public int foodSpawnerAmount;
    [SerializeField] public int randomSeed;

    // converted Entities
    public Entity foodSpawnerEntityPrefab;
    public Entity foodEntityPrefab;

    // food spawner output locations
    private float3[] foodSpawnerOutputLocations;

    // other
    private World defaultWorld;
    private EntityManager entityManager;
    private BlobAssetStore blobAssetStore;

    void Awake() {
        // get world and entity manager to keep lines short
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        // convert prefabs into entities
        blobAssetStore = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, blobAssetStore);
        foodSpawnerEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(foodSpawnerPrefab, settings);
        foodEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(foodPrefab, settings);

        // name entities accordingly
        entityManager.SetName(foodSpawnerEntityPrefab, "foodSpawner");
        entityManager.SetName(foodEntityPrefab, "food");
    }

    void Start() {
        Random.InitState(randomSeed);
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;

        for (int i = 0; i < foodSpawnerAmount; i++) {
            Entity newFoodSpawner = entityManager.Instantiate(foodSpawnerEntityPrefab);
            Vector2 randomDirection = Random.insideUnitCircle * mapRadius;
            float3 randomPoint = new float3(randomDirection.x, 0f, randomDirection.y);
            entityManager.SetComponentData(newFoodSpawner, new Translation {
                Value = randomPoint
            });
        }
    }

    void OnDestroy() {
        blobAssetStore.Dispose();
    }
}
