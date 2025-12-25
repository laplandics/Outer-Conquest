using UnityEngine;
using UnityEngine.InputSystem;

public class GameCameraRayCaster : EntityComponent
{
    private Camera _sceneCamera;

    public override void Initialize(Entity entity)
    {
        _sceneCamera = FindFirstObjectByType<Camera>();
    }
    
    private RaycastHit CastMouseRay()
    {
        var mousePos = Mouse.current.position.ReadValue();
        var mouseRay = _sceneCamera.ScreenPointToRay(mousePos);
        Physics.Raycast(mouseRay, out var mouseRaycastHit);
        return mouseRaycastHit;
    }

    private RaycastHit CastCameraRay()
    {
        var cameraRay = new Ray(_sceneCamera.transform.position, _sceneCamera.transform.forward);
        Physics.Raycast(cameraRay, out var cameraRaycastHit);
        return cameraRaycastHit;
    }
    
    public RaycastHit GetCameraRaycastHit => CastCameraRay();
    public RaycastHit GetMouseRaycastHit => CastMouseRay();
}