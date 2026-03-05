using System;

[Serializable]
public class UnitCommunicator : UnitSystem
{
    public Action<UnitComponent> OnComponentBuilt;
    public Action<UnitComponent> OnComponentRemoved;
    public Action OnUnitHovered;
    public Action OnUnitUnhovered;
    public Action OnUnitSelected;
    public Action OnUnitDeselected;
}