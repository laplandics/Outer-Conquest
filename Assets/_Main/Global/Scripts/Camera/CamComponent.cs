using UnityEngine;

public abstract class CamComponent : MonoBehaviour
{
    public abstract void Initialize(Cam cam);
    public abstract void Deinitialize();
}