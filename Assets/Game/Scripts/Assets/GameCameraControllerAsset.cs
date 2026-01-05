using Unity.Cinemachine;
using UnityEngine;

public class GameCameraControllerAsset : EntityAsset
{
    public CinemachineCamera cmCamera;
    public Transform cameraTarget;
    public CinemachineInputAxisController cmInputAxis;
    public CinemachineOrbitalFollow cmOrbitalFollow;
}