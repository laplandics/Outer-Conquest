using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameplayCam : Cam
{
    public CinemachineFollow cmFollow;
    public CinemachineCamera cmCamera;
    public Transform target;

    public override void OnCreate()
    {
        SetCmCamera();
        SetTarget();
        InitializeComponents();
    }
    
    private void SetCmCamera()
    {
        if (cmCamera != null) return;
        cmCamera = new GameObject("CmCamera").AddComponent<CinemachineCamera>();
        cmCamera.transform.SetParent(transform, false);
        var cmBrain = cameraInstance.gameObject.AddComponent<CinemachineBrain>();
        cmBrain.UpdateMethod = CinemachineBrain.UpdateMethods.LateUpdate;
        cmCamera.Lens.FarClipPlane = settings.maxDistance;
        cmCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
        cmCamera.Lens.OrthographicSize = 8;
    }

    private void SetTarget()
    {
        target = new GameObject("Target").transform;
        target.SetParent(transform, false);
        cmFollow = cmCamera.gameObject.AddComponent<CinemachineFollow>();
        cmCamera.Follow = target;
        cmFollow.FollowOffset = new Vector3(0, 10, 0);
    }

    private void InitializeComponents()
    { foreach (var component in components) { component.Initialize(this); } }
    
    private void DeinitializeComponents()
    { foreach (var component in components) { component.Deinitialize(); } }
    
    public override void OnDestruct()
    {
        DeinitializeComponents();
    }
}