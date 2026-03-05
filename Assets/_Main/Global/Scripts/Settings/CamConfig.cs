using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Cam", menuName = "Config/Camera")]
public class CamConfig : Config
{
    [Header("Common")]
    public CamSettings[] settings;
    
    [Header("Game")]
    public float moveSpeed;
    public float dragSpeed;
    public float zoomSpeed;
    public Vector2 zoomRange;
}

[Serializable]
public class CamSettings
{
    public CamType camType;
    public float maxDistance;
    public Color backgroundColor;
    public CamComponentType[] components;
}