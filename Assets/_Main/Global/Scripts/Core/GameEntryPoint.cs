using System.Collections;
using UnityEngine;

public class GameEntryPoint
{
    private static GameEntryPoint _instance;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Boot() { _instance = new GameEntryPoint(); }

    private GameEntryPoint()
    {
        Application.targetFrameRate = 144;
        
        G.Input.Clear();
        G.Input.Set();
        
        G.Coroutine.Destroy();
        G.Coroutine.Launch();
        
        G.Update.Destroy();
        G.Update.Launch();
        
        G.Debug.Destroy();
        G.Debug.Launch();
        
        G.Ui.Destroy();
        G.Ui.Launch();
        
        G.Event.ClearSubscribers();
        
        G.Coroutine.Start(StartGame());
    }
    
    private IEnumerator StartGame()
    {
        yield return G.Scene.LoadScene(G.Scene.GAME);
        var gameEntryPoint = new GameObject("EntryPoint").AddComponent<SceneEntryPoint>();
        yield return gameEntryPoint.StartScene();
        Debug.Log("Game Started");
    }
}