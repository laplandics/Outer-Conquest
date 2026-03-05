using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Cam : MonoBehaviour, IEntity
{
    private CamData _camData;
    public CamConfig config;
    public CamSettings settings;
    public Camera cameraInstance;
    public CamComponent[] components;
    public UniversalAdditionalCameraData cameraData;
    
    public GameObject Instance => gameObject;
    
    public void Create(EntityData data)
    {
        if (data is not CamData newData)
        { throw new Exception("Entity data is not a CamData"); }
        _camData = newData;
        config = G.Load.LoadConfig<CamConfig>(nameof(Cam));
        foreach (var s in config.settings)
        { if (s.camType.ToString() == GetType().Name) settings = s; }
        
        cameraInstance = new GameObject("Camera").AddComponent<Camera>();
        cameraInstance.transform.SetParent(transform, false);
        cameraInstance.orthographic = true;
        cameraInstance.farClipPlane = settings.maxDistance;
        cameraInstance.clearFlags = CameraClearFlags.SolidColor;
        cameraInstance.backgroundColor = settings.backgroundColor;

        cameraData = cameraInstance.GetUniversalAdditionalCameraData();
        cameraData.renderPostProcessing = true;
        cameraData.antialiasing = AntialiasingMode.TemporalAntiAliasing;
        cameraData.taaSettings.quality = TemporalAAQuality.High;
        
        components = new CamComponent[settings.components.Length];
        for (var i = 0; i < settings.components.Length; i++)
        {
            var componentName = settings.components[i].ToString();
            var componentType = Type.GetType(componentName);
            var component = gameObject.AddComponent(componentType);
            components[i] = (CamComponent)component;
        }
        
        if (_camData.isMain) cameraInstance.tag = "MainCamera";
        OnCreate();
    }
    
    public abstract void OnCreate();

    public void Destruct() { OnDestruct(); }

    public abstract void OnDestruct();
}