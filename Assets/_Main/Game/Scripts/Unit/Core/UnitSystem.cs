using System;

[Serializable]
public abstract class UnitSystem
{
    [ReadOnly] public string systemName;
    protected Unit Owner;
    
    public virtual void Initialize(Unit unit) => Owner = unit;
    public virtual void Deinitialize() => Owner = null;
}