using UnityEngine;

public abstract class Service : ScriptableObject
{
    public virtual void ServiceAwake() { }
    public virtual void ServiceStart() { }
    public virtual void ServiceStop() { }
}