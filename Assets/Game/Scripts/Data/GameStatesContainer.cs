using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStates", menuName = "GameData/GameStates")]
public class GameStatesContainer : DataContainer
{
    public override Type GetAssetType() => typeof(GameState);
    
    [SerializeField] private SceneStatus sceneStatus;
    
    public List<GameState> GetAllStates => new() {sceneStatus};
}