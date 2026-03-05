using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamZoom : CamComponent
{
    private GameplayCam _gmCam;
    private CinemachineCamera _cmCamera;
    private CamConfig _camConfig;
    private GameInputs _inputs;
    
    public override void Initialize(Cam cam)
    {
        if (cam is not GameplayCam gmCam) return;
        _gmCam = gmCam;
        _cmCamera = _gmCam.cmCamera;
        _camConfig = _gmCam.config;
        _inputs = G.Input.Get;
        _inputs.Camera.Zoom.Enable();
        _inputs.Camera.Zoom.performed += OnZoom;
    }

    private void OnZoom(InputAction.CallbackContext ctx)
    {
        var zoomDelta = ctx.ReadValue<float>();
        var zoom = zoomDelta * _camConfig.zoomSpeed;
        var zoomValue = Mathf.Lerp
        (_cmCamera.Lens.OrthographicSize, zoom, Time.deltaTime);
        _cmCamera.Lens.OrthographicSize = Mathf.Clamp
        (zoomValue, _camConfig.zoomRange.x, _camConfig.zoomRange.y);
    }
    
    public override void Deinitialize()
    {
        _inputs.Camera.Zoom.performed -= OnZoom;
        _inputs.Camera.Zoom.Disable();
        _inputs = null;
        _gmCam = null;
        _cmCamera = null;
    }
}