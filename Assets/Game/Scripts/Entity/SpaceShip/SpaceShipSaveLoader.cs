public class SpaceShipSaveLoader : EntitySaveLoader
{
    public override void Enable() => LoadComponents<SpaceShipData>();
}