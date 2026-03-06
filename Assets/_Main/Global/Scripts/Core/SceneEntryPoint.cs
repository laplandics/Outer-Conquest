using System.Collections;
using G;
using UnityEngine;

public class SceneEntryPoint : MonoBehaviour
{
    private Service[] _services;
    
    public IEnumerator StartScene()
    {
        yield return LoadServices();
        G.Input.Enable();
    }

    public IEnumerator EndScene()
    {
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