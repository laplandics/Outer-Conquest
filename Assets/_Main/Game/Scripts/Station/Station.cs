public class Station : Unit
{
    public override void OnCreate()
    {
        CreateSystem<UnitInitializer>();
        CreateSystem<UnitConstructor>();
        CreateSystem<UnitColliderBuilder>();
        CreateSystem<UnitSelector>();
        CreateSystem<UnitInteractor>();
        CreateSystem<UnitCommunicator>();
        
        StartSystems();
        
        FindSystem<UnitInitializer>().BeginInitialization();
    }

    public override void OnDestruct()
    {
        EndSystems();
        
        DeleteSystem<UnitInitializer>();
        DeleteSystem<UnitConstructor>();
        DeleteSystem<UnitColliderBuilder>();
        DeleteSystem<UnitSelector>();
        DeleteSystem<UnitInteractor>();
        DeleteSystem<UnitCommunicator>();
    }
}