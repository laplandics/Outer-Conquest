using UnityEngine;

[CreateAssetMenu(fileName = "CameraController", menuName = "Prefs/CameraController")]
public class CameraControllerPrefs: ComponentPrefs
{
    public float zoomSpeed; 
    public float minZoom; 
    public float maxZoom; 
    public float moveSpeed;
}