using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

/// <summary>
/// Converts GameObject prefabs into Entity prefabs.
/// </summary>
public class PrefabConverter : MonoBehaviour
{

    // GameObject prefabs
    [SerializeField] GameObject foodSpawnerPrefab;
    [SerializeField] GameObject foodPrefab;

    // converted Entities
    public Entity foodSpawnerEntityPrefab;
    public Entity foodEntityPrefab;

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
        entityManager.SetName(foodSpawnerEntityPrefab, "foodSpawnerEntityPrefab");
        entityManager.SetName(foodEntityPrefab, "foodEntityPrefab");
    }

    void OnDestroy() {
        blobAssetStore.Dispose();
    }
}
