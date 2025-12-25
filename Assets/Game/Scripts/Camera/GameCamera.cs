using System;
using UnityEngine;

public class GameCamera : Entity
{

}

[Serializable]
public class GameCameraData : EntityData
{
    public Vector3 targetPosition;
    public float zoom;
    public float horizontalRotation;
    public float verticalRotation;
}