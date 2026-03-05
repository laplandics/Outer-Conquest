using UnityEngine;

public interface IEntity
{
    public GameObject Instance { get; }
    
    public void Create(EntityData data);
    public void OnCreate();
    
    public void Destruct();
    public void OnDestruct();
}