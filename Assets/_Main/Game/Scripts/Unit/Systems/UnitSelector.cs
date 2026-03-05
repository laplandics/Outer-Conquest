using System;

[Serializable]
public class UnitSelector : UnitSystem
{
    private UnitCommunicator _communicator;

    public override void Initialize(Unit unit)
    { base.Initialize(unit); _communicator = Owner.FindSystem<UnitCommunicator>(); }

    public virtual void OnHoverEnter()
    {
        _communicator.OnUnitHovered?.Invoke();
    }

    public virtual void OnHoverExit()
    {
        _communicator.OnUnitUnhovered?.Invoke();
    }

    public virtual void OnSelect()
    {
        _communicator.OnUnitSelected?.Invoke();
    }

    public virtual void OnDeselect()
    {
        _communicator.OnUnitDeselected?.Invoke();
    }

    public override void Deinitialize()
    { base.Deinitialize(); _communicator = null; }
}