using UnityEngine;

[CreateAssetMenu(fileName = "UnitService", menuName = "Service/Unit")]
public class UnitService : Service
{
    private UnitSelectionHandler _selector;

    public override void ServiceStart()
    { _selector = new UnitSelectionHandler();}
    
    public override void ServiceStop()
    { _selector.Dispose(); _selector = null; }
}