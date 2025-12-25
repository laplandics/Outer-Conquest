using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameServices", menuName = "GameData/GameServices")]
public class GameServicesContainer : DataContainer
{
    public override Type GetAssetType() => typeof(GameService);
    
    [SerializeField] private InputService inputService;
    
    public List<GameService> GetAllServices => new() {inputService};
}