using UnityEngine;

[CreateAssetMenu(fileName = "CameraPrefs", menuName = "Prefs/CameraPrefs")]
public class CameraPrefs: Preferences
{
    public float zoomSpeed; 
    public float minZoom; 
    public float maxZoom; 
    public float moveSpeed;
}