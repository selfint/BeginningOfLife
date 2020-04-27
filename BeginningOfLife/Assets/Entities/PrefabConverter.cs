using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PrefabConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {

    // add these in the prefab converter script IN THE HIERARCHY, NOT THE PREFAB
    public GameObject soupSpotPrefab;
    public static Entity soupSpotEntity;
    public GameObject foodPrefab;
    public static Entity foodEntity;


    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {

        // TODO: make this more clean
        PrefabConverter.soupSpotEntity = conversionSystem.GetPrimaryEntity(soupSpotPrefab);
        PrefabConverter.foodEntity = conversionSystem.GetPrimaryEntity(foodPrefab);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {

        // TODO: make this more clean
        referencedPrefabs.Add(soupSpotPrefab);
        referencedPrefabs.Add(foodPrefab);
    }
}
