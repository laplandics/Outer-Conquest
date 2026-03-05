using System;

[Serializable]
public class UnitInteractor : UnitSystem, IUiUser
{
    private UnitCommunicator _communicator;
    private UnitUi _ui;
    
    public override void Initialize(Unit unit)
    {
        base.Initialize(unit);
        _communicator = Owner.FindSystem<UnitCommunicator>();
        _communicator.OnUnitSelected += ShowUi;
        _communicator.OnUnitDeselected += HideUi;
    }

    public (string name, Action<object> action)[] GetActions() { return null; }

    public void ShowUi() { _ui = G.Ui.Create<UnitUi>(this); }
    public void HideUi() { G.Ui.Delete(_ui); }

    public override void Deinitialize()
    {
        base.Deinitialize();
        _communicator.OnUnitSelected -= ShowUi;
        _communicator.OnUnitDeselected -= HideUi;
    }
}