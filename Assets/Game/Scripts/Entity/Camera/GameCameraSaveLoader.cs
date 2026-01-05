public class GameCameraSaveLoader : EntitySaveLoader
{
    public override void Enable() => LoadComponents<GameCameraData>();
}