using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefabs", menuName = "GameData/Prefabs")]
public class PrefabsContainer : DataContainer
{
    public override Type GetAssetType() => typeof(GameObject);
    
    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private GameObject cmCamera;
    [SerializeField] private GameObject motherShipMk0;
    
    public List<GameObject> GetAllPrefabs => new() {cameraTarget, cmCamera, motherShipMk0};
}