using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCameraController : EntityComponent
{
    [SerializeField] private CinemachineCamera cmCamera;
    [SerializeField] private Transform target;
    [SerializeField] private CinemachineInputAxisController cmController;
    [SerializeField] private CinemachineOrbitalFollow cmOrbitalFollow;
    private GameCameraData _data;
    private CameraControllerPrefs _prefs;
    private Transform _cameraTransform;
    private GameInputs _inputs;
    private LensSettings.OverrideModes _mode;
    private Vector3 _inputDir;

    public override void Initialize(Entity entity)
    {
        owner = entity;
        _data = owner.GetData<GameCameraData>();
        cmController.enabled = false;
        _cameraTransform = cmCamera.transform;
        G.GetManager<RoutineManager>().StartLateUpdateAction(UpdateTarget);
        _inputs = G.GetService<InputService>().GetGameInputs();
        _inputs.Camera.Enable();
        _inputs.Camera.Position.performed += ReadMoveVector;
        _inputs.Camera.Position.canceled += ReadMoveVector;
        _inputs.Camera.RotationSwitcher.performed += SwitchRotation;
        _inputs.Camera.RotationSwitcher.canceled += SwitchRotation;
        _inputs.Camera.Zoom.performed += UpdateZoom;
        _inputs.Camera.Zoom.canceled += UpdateZoom;
        SetInitialState();
    }
    
    private void SetInitialState()
    {
        target.position = _data.targetPosition;
        cmOrbitalFollow.Radius = _data.zoom;
        cmOrbitalFollow.HorizontalAxis.Value = _data.horizontalRotation;
        cmOrbitalFollow.VerticalAxis.Value = _data.verticalRotation;
    }
    
    private void ReadMoveVector(InputAction.CallbackContext ctx)
    {
        _inputDir = Vector2.zero;
        _inputDir.x = ctx.ReadValue<Vector2>().x;
        _inputDir.z = ctx.ReadValue<Vector2>().y;
    }
    
    private void SwitchRotation(InputAction.CallbackContext input) { cmController.enabled = input.performed; }
    
    private void UpdateZoom(InputAction.CallbackContext input)
    {
        var zoomValue = input.ReadValue<float>();
        var currentZoom = cmOrbitalFollow.Radius;
        cmOrbitalFollow.Radius = Mathf.Clamp(
            currentZoom + zoomValue * _prefs.zoomSpeed,
            _prefs.minZoom, _prefs.maxZoom);
    }
    
    private void UpdateTarget()
    {
        target.rotation = _cameraTransform.rotation;
        var moveDir = target.up * _inputDir.z + target.right * _inputDir.x;
        moveDir.y = 0;
        target.position += moveDir * (_prefs.moveSpeed * Time.deltaTime);
    }
    
    public (Vector3 targetPosition, float cameraHorizontalRotation, float cameraVerticalRotation, float cameraZoom) GetCameraState()
    {
        var pos = target.position;
        var horizontal = cmOrbitalFollow.HorizontalAxis.Value;
        var vertical = cmOrbitalFollow.VerticalAxis.Value;
        var zoom = cmOrbitalFollow.Radius;
        return (pos, horizontal, vertical, zoom);
    }
    
    public override void Disable()
    {
        _inputs.Camera.Position.performed -= ReadMoveVector;
        _inputs.Camera.Position.canceled -= ReadMoveVector;
        _inputs.Camera.RotationSwitcher.performed -= SwitchRotation;
        _inputs.Camera.RotationSwitcher.canceled -= SwitchRotation;
        _inputs.Camera.Zoom.performed -= UpdateZoom;
        _inputs.Camera.Zoom.canceled -= UpdateZoom;
        _inputs.Camera.Disable();
    }
}