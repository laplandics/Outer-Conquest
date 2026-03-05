using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    [ReadOnly] public Unit owner;

    public virtual void Initialize(Unit unit)
    { owner = unit; owner.FindSystem<UnitCommunicator>().OnComponentBuilt?.Invoke(this); }

    public virtual void Deinitialize()
    { owner.FindSystem<UnitCommunicator>().OnComponentRemoved?.Invoke(this); }
}