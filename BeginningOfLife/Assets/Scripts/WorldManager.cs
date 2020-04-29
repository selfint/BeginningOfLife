using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Physics;
using Unity.Collections;
using Random = UnityEngine.Random;

/// <summary>
/// This MonoBehaviour generates the world and
/// any new entities that need to be created
/// </summary>
public class WorldManager : MonoBehaviour {

    // general configuration
    [Header("Change simulation speed")]
    [Range(1f, 10f)]
    [SerializeField] public float timeScale = 1f;

    // GameObject prefabs
    [SerializeField] GameObject foodSpawnerPrefab;
    [SerializeField] GameObject foodPrefab;
    [SerializeField] GameObject mapGameObject;

    // World generate configuration
    [SerializeField] public float mapRadius;
    [SerializeField] public float mapHeight;
    [SerializeField] public int foodSpawnerAmount;
    [SerializeField] public int randomSeed;

    // converted Entities
    public Entity foodSpawnerEntityPrefab;
    public Entity foodEntityPrefab;
    public Entity mapEntity;

    // food spawning
    [SerializeField] int foodSpawnRate;
    private float3[] foodSpawnerOutputLocations;
    private float foodSpawningCounter;

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
        Entity mapEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(mapGameObject, settings);
        mapEntity = entityManager.Instantiate(mapEntityPrefab);
        initializeMap();

        // name entities accordingly
        entityManager.SetName(foodSpawnerEntityPrefab, "foodSpawner");
        entityManager.SetName(foodEntityPrefab, "food");
        entityManager.SetName(mapEntity, "map");

        // initialize arrays
        foodSpawnerOutputLocations = new float3[foodSpawnerAmount];
    }

    void Start() {
        if (randomSeed != 0)
            Random.InitState(randomSeed);

        // food spawning
        spawnFoodSpawners();
        foodSpawningCounter = 0;
    }

    void initializeMap() {
        entityManager.SetComponentData(mapEntity, new Translation { Value = float3.zero });

        // scale map
        float4x4 newScale = entityManager.GetComponentData<CompositeScale>(mapEntity).Value;

        // add 0.001 so nothing escapes below the map
        newScale.c0.x = mapRadius * 2.5f;
        newScale.c2.z = mapRadius * 2.5f;
        entityManager.SetComponentData(mapEntity, new CompositeScale {
            Value = newScale
        });

        // add MapData component
        entityManager.AddComponentData(mapEntity, new MapData {
            mapRadius = mapRadius,
            mapHeight = mapHeight
        });
    }

    void spawnFoodSpawners() {
        for (int i = 0; i < foodSpawnerAmount; i++) {
            Entity newFoodSpawner = entityManager.Instantiate(foodSpawnerEntityPrefab);
            Vector2 randomDirection = Random.insideUnitCircle * mapRadius;
            float3 randomPoint = new float3(randomDirection.x, 0f, randomDirection.y);
            entityManager.SetComponentData(newFoodSpawner, new Translation {
                Value = randomPoint
            });
            foodSpawnerOutputLocations[i] = randomPoint + entityManager.GetComponentData<FoodOutputLocation>(newFoodSpawner).Value;
        }
    }

    void Update() {
        setTimeScale();
        SpawnFoods();
    }

    void setTimeScale() {
        UnityEngine.Time.timeScale = timeScale;
        Time.timeScale = timeScale;
    }

    void SpawnFoods() {
        if (foodSpawningCounter >= foodSpawnRate) {

            // TODO: maybe make food spawners not all spawn foods at once?
            foodSpawningCounter = 0;
        foreach (float3 outptLocation in foodSpawnerOutputLocations) {
            Entity newFood = entityManager.Instantiate(foodEntityPrefab);
            entityManager.SetComponentData(newFood, new Translation {
                Value = outptLocation
            });

            // send food in a random trajectory upwards
            Vector2 randomDirection = Random.insideUnitCircle;
            float upwardsForce = Random.value;
            float3 randomFoodDirection = new float3(randomDirection.x, upwardsForce, randomDirection.y);
            entityManager.SetComponentData(newFood, new PhysicsVelocity {
                Linear = randomFoodDirection
            });
        }
        } else {
            foodSpawningCounter += Time.deltaTime;
        }
    }

    void OnDestroy() {
        blobAssetStore.Dispose();
    }
}
