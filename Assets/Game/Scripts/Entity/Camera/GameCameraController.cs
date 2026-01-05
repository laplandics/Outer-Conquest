using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCameraController : EntityController, ILoadingComponent<GameCameraData>
{
    [SerializeField] private GameCameraControllerAsset gmControllerAsset;
    [SerializeField] private CameraPrefs cameraPrefs;
    
    private CinemachineCamera _cmCamera;
    private Transform _cameraTarget;
    private CinemachineInputAxisController _cmController;
    private CinemachineOrbitalFollow _cmOrbitalFollow;
    
    private Transform _cameraTransform;
    private GameInputs _inputs;
    private Vector3 _inputDir;

    public override void Initialize(Entity entity)
    {
        owner = entity;
        cameraPrefs = G.Data().GetAsset<Preferences>(nameof(CameraPrefs)) as CameraPrefs;
        var controllerPrefab = G.Data().GetAsset<EntityAsset>(nameof(GameCameraControllerAsset));
        gmControllerAsset = Spawner.Spawn(controllerPrefab, Vector3.zero, Quaternion.identity).GetComponent<GameCameraControllerAsset>();
        gmControllerAsset.transform.SetParent(transform, false);
        _cmCamera = gmControllerAsset.cmCamera;
        _cameraTarget = gmControllerAsset.cameraTarget;
        _cmController = gmControllerAsset.cmInputAxis;
        _cmOrbitalFollow = gmControllerAsset.cmOrbitalFollow;
        _cameraTransform = _cmCamera.transform;
        G.GetManager<RoutineManager>().StartLateUpdateAction(UpdateTarget);
        _inputs = G.GetService<InputService>().GetGameInputs();
        _inputs.Camera.Enable();
        _inputs.Camera.Position.performed += ReadMoveVector;
        _inputs.Camera.Position.canceled += ReadMoveVector;
        _inputs.Camera.RotationSwitcher.performed += SwitchRotation;
        _inputs.Camera.RotationSwitcher.canceled += SwitchRotation;
        _inputs.Camera.Zoom.performed += UpdateZoom;
        _inputs.Camera.Zoom.canceled += UpdateZoom;
    }

    public void Load(GameCameraData data)
    {
        _cameraTarget.position = data.targetPosition;
        _cmOrbitalFollow.Radius = data.zoom;
        _cmOrbitalFollow.HorizontalAxis.Value = data.horizontalRotation;
        _cmOrbitalFollow.VerticalAxis.Value = data.verticalRotation;
    }
    
    private void ReadMoveVector(InputAction.CallbackContext ctx)
    {
        _inputDir = Vector2.zero;
        _inputDir.x = ctx.ReadValue<Vector2>().x;
        _inputDir.z = ctx.ReadValue<Vector2>().y;
    }
    
    private void SwitchRotation(InputAction.CallbackContext input) { _cmController.enabled = input.performed; }
    
    private void UpdateZoom(InputAction.CallbackContext input)
    {
        var zoomValue = input.ReadValue<float>();
        var currentZoom = _cmOrbitalFollow.Radius;
        _cmOrbitalFollow.Radius = Mathf.Clamp(
            currentZoom + zoomValue * cameraPrefs.zoomSpeed,
            cameraPrefs.minZoom, cameraPrefs.maxZoom);
    }
    
    private void UpdateTarget()
    {
        _cameraTarget.rotation = _cameraTransform.rotation;
        var moveDir = _cameraTarget.up * _inputDir.z + _cameraTarget.right * _inputDir.x;
        moveDir.y = 0;
        _cameraTarget.position += moveDir * (cameraPrefs.moveSpeed * Time.deltaTime);
    }
    
    public (Vector3 targetPosition, float cameraHorizontalRotation, float cameraVerticalRotation, float cameraZoom) GetCameraState()
    {
        var pos = _cameraTarget.position;
        var horizontal = _cmOrbitalFollow.HorizontalAxis.Value;
        var vertical = _cmOrbitalFollow.VerticalAxis.Value;
        var zoom = _cmOrbitalFollow.Radius;
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