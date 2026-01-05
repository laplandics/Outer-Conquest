
public class EntitySpawnManager : SceneManager, IEndListener, IStartListener
{
    private GameCamera _cameraEntity;
    private SpaceShip _spaceShipEntity;
    private SpaceShip _spaceShip2Entity;

    public void OnStart()
    {
        _cameraEntity = E.NewEntity<GameCamera>(G.GetPreferences<GlobalPrefs>().initialCameraData);
        _spaceShipEntity = E.NewEntity<SpaceShip>(G.GetPreferences<GlobalPrefs>().initialSpaceShipData);
        _spaceShip2Entity = E.NewEntity<SpaceShip>(G.GetPreferences<GlobalPrefs>().initialSpaceShip2Data);
    }
    
    public void OnEnd()
    {
        E.DeleteEntity(_cameraEntity);
        E.DeleteEntity(_spaceShipEntity);
    }
}