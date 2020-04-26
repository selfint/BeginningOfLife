using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PrefabConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {

    public GameObject soupSpawnerPrefab;
    public static Entity soupSpawnerEntity;
    public GameObject foodPrefab;
    public static Entity foodEntity;


    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {

        // TODO: make this more clean
        PrefabConverter.soupSpawnerEntity = conversionSystem.GetPrimaryEntity(soupSpawnerPrefab);
        PrefabConverter.foodEntity = conversionSystem.GetPrimaryEntity(foodPrefab);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {

        // TODO: make this more clean
        referencedPrefabs.Add(soupSpawnerPrefab);
        referencedPrefabs.Add(foodPrefab);
    }
}
