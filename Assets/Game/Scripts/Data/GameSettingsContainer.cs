using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameData/Settings")]
public class GameSettingsContainer : DataContainer
{
    public override Type GetAssetType() => typeof(GameSettings);
    
    [SerializeField] private GlobalSettings globalSettings;
    
    public List<GameSettings> GetAllSettings => new() {globalSettings};
}