using System.Collections;
using G;
using UnityEngine;

public class SceneEntryPoint : MonoBehaviour
{
    private Service[] _services;
    private Entity<Cam> _mainCameraEntity;
    
    public IEnumerator StartScene()
    {
        CreateMainCamera();
        yield return LoadServices();
        G.Input.Enable();
    }

    private void CreateMainCamera()
    {
        var camData = Data.NewGameCamera(CamType.GameplayCam);
        _mainCameraEntity = new Entity<Cam>(camData);
    }

    public IEnumerator EndScene()
    {
        _mainCameraEntity.Destroy();
        G.Input.Disable();
        yield return UnloadServices();
    }

    private IEnumerator LoadServices()
    {
        _services = Resources.LoadAll<Service>(nameof(Service));
        if (_services == null || _services.Length == 0) yield break;
        foreach (var service in _services) service.ServiceAwake();
        yield return null;
        foreach (var service in _services) service.ServiceStart();
        yield return null;
    }

    private IEnumerator UnloadServices()
    {
        if (_services == null || _services.Length == 0) yield break;
        foreach (var service in _services) service.ServiceStop();
        yield return null;
    }
}