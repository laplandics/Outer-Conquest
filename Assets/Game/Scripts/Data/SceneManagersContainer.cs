using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneManagers", menuName = "GameData/SceneManagers")]
public class SceneManagersContainer : DataContainer
{
    public override Type GetAssetType() => typeof(SceneManager);

    [SerializeField] private SceneManager entityManager;
    [SerializeField] private SceneManager routineManager;
    [SerializeField] private SceneManager selectionManager;
    
    public List<SceneManager> GetAllSceneManagers => new() {entityManager, routineManager, selectionManager};
}