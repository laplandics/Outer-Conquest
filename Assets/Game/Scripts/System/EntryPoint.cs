using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform managersContainer;
    [SerializeField] private GDC globalDataContainer;
    
    private List<GameSettings> _gameSettings = new();
    private List<GameState> _gameStates = new();
    private List<GameService> _gameServices = new();
    private List<SceneManager> _sceneManagers = new();

    private List<SceneManager> _managersPrefabs = new();

    private IEnumerator Start()
    {
        Application.quitting += End;
        yield return InitializeAssets();
        yield return GetSettings();
        yield return LoadGameStates();
        yield return RunServices();
        yield return InitializeManagers();
        AnnounceOnStart();
        Eventer.Invoke(new SceneStarted());
    }

    private IEnumerator InitializeAssets() 
    {
        yield return globalDataContainer.Initialize();
        G.CacheData(globalDataContainer);
        _gameSettings = G.Data().GetData<GameSettingsContainer>().GetAllSettings;
        _gameStates = G.Data().GetData<GameStatesContainer>().GetAllStates;
        _gameServices = G.Data().GetData<GameServicesContainer>().GetAllServices;
        _managersPrefabs = G.Data().GetData<SceneManagersContainer>().GetAllSceneManagers;
        foreach (var prefab in _managersPrefabs)
        {
            Debug.Log(prefab.name);
        }
    }

    private IEnumerator GetSettings() { G.CacheGameSettings(_gameSettings); yield break; }

    private IEnumerator LoadGameStates()
    {
        foreach (var gameState in _gameStates) { yield return gameState.Load(); }
        G.CacheGameStates(_gameStates);
        Eventer.Invoke(new StatesLoaded());
    }
    
    private IEnumerator RunServices()
    {
        foreach (var gameService in _gameServices) { yield return gameService.Run(); }
        G.CacheGameServices(_gameServices);
        Eventer.Invoke(new ServicesLaunched());
    }
    
    private IEnumerator InitializeManagers()
    {
        _sceneManagers = new List<SceneManager>();
        foreach (var prefab in _managersPrefabs)
        {
            var managerInstance = Spawner.Spawn(prefab, Vector3.zero, Quaternion.identity, managersContainer);
            managerInstance.gameObject.name = prefab.name;
            _sceneManagers.Add(managerInstance);
            yield return managerInstance.Initialize();
        }
        _managersPrefabs.Clear();
        G.CacheSceneManagers(_sceneManagers);
        Eventer.Invoke(new ManagersInitialized());
    }

    private void AnnounceOnStart()
    {
        var allSystemClasses = new List<Object>();
        allSystemClasses.AddRange(_gameStates);
        allSystemClasses.AddRange(_gameServices);
        allSystemClasses.AddRange(_sceneManagers);
        foreach (var systemClass in allSystemClasses) 
        { if (systemClass is IStartListener startListener) startListener.OnStart(); }
    }

    private void End()
    {
        Application.quitting -= End;
        AnnounceOnEnd();
        Eventer.Invoke(new SceneEnded());
        DeinitializeManagers();
        StopServices();
        UnloadStates();
        Eventer.ClearSubscribers();
        G.ResetData();
    }
    
    private void AnnounceOnEnd()
    {
        var allSystemClasses = new List<Object>();
        allSystemClasses.AddRange(_gameStates);
        allSystemClasses.AddRange(_gameServices);
        allSystemClasses.AddRange(_sceneManagers);
        foreach (var systemClass in allSystemClasses)
        { if (systemClass is IEndListener endListener) endListener.OnEnd(); }
    }

    private void DeinitializeManagers()
    {
        foreach (var manager in _sceneManagers) { manager.Deinitialize(); }
        _sceneManagers.Clear();
        Eventer.Invoke(new ManagersDeinitialized());
    }
    
    private void StopServices()
    {
        foreach (var service in _gameServices) { service.Stop(); }
        _gameServices.Clear();
        Eventer.Invoke(new ServicesStopped());
    }

    private void UnloadStates()
    {
        foreach (var state in _gameStates) { state.Unload(); }
        _gameStates.Clear();
        Eventer.Invoke(new StatesUnloaded());
    }
}
