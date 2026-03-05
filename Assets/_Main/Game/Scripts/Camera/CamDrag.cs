using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamDrag : CamComponent
{
    private GameplayCam _gmCam;
    private Camera _cam;
    private CamConfig _config;
    private Transform _target;
    private CinemachineFollow _follow;
    private GameInputs _inputs;
    private Vector3 _dragOrigin;

    public override void Initialize(Cam cam)
    {
        if (cam is not GameplayCam gmCam) return;
        _gmCam = gmCam;
        _cam = _gmCam.cameraInstance;
        _config = _gmCam.config;
        _target = _gmCam.target;
        _follow = _gmCam.cmCamera.GetComponent<CinemachineFollow>();
        _inputs = G.Input.Get;
        G.Update.SetNewUpdate(Drag);
    }
    
    private void Drag()
    {
        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            _inputs.Camera.Move.Disable();
            _follow.TrackerSettings.PositionDamping = Vector3.zero;
            _target.position = new Vector3(_follow.transform.position.x, 0, _follow.transform.position.z);
            _dragOrigin = GetMouseWorldPos();
        }
        
        if (Mouse.current.middleButton.isPressed)
        {
            var currentPos = GetMouseWorldPos();
            var diff = _dragOrigin - currentPos;
            _target.transform.position += diff * (Time.deltaTime * _config.dragSpeed);
        }
        else
        {
            if (_follow.TrackerSettings.PositionDamping == Vector3.zero)
            {_follow.TrackerSettings.PositionDamping = Vector3.one;}
            if (!_inputs.Camera.Move.enabled) _inputs.Camera.Move.Enable();
        }
        
    }

    private Vector3 GetMouseWorldPos()
    {
        var mousePos = Mouse.current.position.ReadValue();
        var ray = _cam.ScreenPointToRay(mousePos);
        var plane = new Plane(Vector3.up, Vector3.zero);
        return plane.Raycast(ray, out var distance) ? ray.GetPoint(distance) : Vector3.zero;
    }

    public override void Deinitialize()
    {
        G.Update.Forget(Drag);
        _cam = null;
        _config = null;
        _target = null;
        _follow = null;
        _inputs = null;
        _dragOrigin = Vector3.zero;
    }
}