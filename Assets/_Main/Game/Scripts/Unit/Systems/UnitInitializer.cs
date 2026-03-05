using System;

[Serializable]
public class UnitInitializer : UnitSystem
{
    public void BeginInitialization()
    {
        var data = Owner.GetData();
        var constructor = Owner.FindSystem<UnitConstructor>();
        Owner.transform.position = data.position;
    }
}