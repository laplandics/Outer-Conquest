using UnityEngine;

public class CamMove : CamComponent
{
    private GameplayCam _gmCam;
    private CamConfig _config;
    private GameInputs _inputs;
    private Transform _target;
    
    public override void Initialize(Cam cam)
    {
        if (cam is not GameplayCam gmCam) return;
        _gmCam = gmCam;
        _config = _gmCam.config;
        _inputs = G.Input.Get;
        _inputs.Camera.Move.Enable();
        _target = _gmCam.target;
        G.Update.SetNewUpdate(Move);
    }

    private void Move()
    {
        var dir = _inputs.Camera.Move.ReadValue<Vector2>().normalized;
        _target.transform.position += new Vector3(dir.x, 0, dir.y) * (GetSpeed() * Time.deltaTime);
    }

    private float GetSpeed()
    {
        var baseSpeed = _config.moveSpeed;
        var zoom = _gmCam.cmCamera.Lens.OrthographicSize * 0.1f;
        return baseSpeed * zoom;
    }

    public override void Deinitialize()
    {
        G.Update.Forget(Move);
        _inputs.Camera.Move.Disable();
        _inputs = null;
        _config = null;
        _target = null;
    }
}
