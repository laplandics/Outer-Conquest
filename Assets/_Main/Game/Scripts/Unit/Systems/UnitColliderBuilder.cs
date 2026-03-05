using System;
using UnityEngine;

[Serializable]
public class UnitColliderBuilder : UnitSystem
{
    public override void Initialize(Unit unit)
    {
        base.Initialize(unit);
        var observer = unit.FindSystem<UnitCommunicator>();
        observer.OnComponentBuilt += RebuildCollider;
    }

    private void RebuildCollider(UnitComponent _)
    {
        if (Owner.GetComponent<Rigidbody>() == null)
        {
            var rigidbody = Owner.gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
        }
        
        
    }
}