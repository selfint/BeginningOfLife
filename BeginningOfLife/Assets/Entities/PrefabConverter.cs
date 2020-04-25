using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PrefabConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {

    public GameObject soupSpawnerPrefab;
    public static Entity soupSpawnerEntity;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        PrefabConverter.soupSpawnerEntity = conversionSystem.GetPrimaryEntity(soupSpawnerPrefab);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
        referencedPrefabs.Add(soupSpawnerPrefab);
    }
}
