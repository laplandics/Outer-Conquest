using UnityEngine;

public abstract class Cam : MonoBehaviour, IEntity
{
    public GameObject Instance => gameObject;
    
    public void Create(EntityData data) { }
    
    public abstract void OnCreate();

    public void Destruct() { OnDestruct(); }

    public abstract void OnDestruct();
}