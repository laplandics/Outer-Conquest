using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityComponentsPrefs", menuName = "GameData/EntityComponentPrefs")]
public class EntityComponentsPrefsContainer : DataContainer
{
    public override Type GetAssetType() => typeof(ComponentPrefs);
    
    [SerializeField] private CameraControllerPrefs cameraControllerPrefs;
    
    public List<ComponentPrefs> GetAllComponents => new() {cameraControllerPrefs};
}