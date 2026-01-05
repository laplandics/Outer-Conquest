using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "InputService", menuName = "Services/InputService")]
public class InputService : GameService, IStartListener
{
    private GameInputs _gameInputs;
    
    public override IEnumerator Run() { _gameInputs = new GameInputs(); yield break; }
    
    public void OnStart() { EnableInputs(); }
    public GameInputs GetGameInputs() => _gameInputs;
    public void DisableInputs() => _gameInputs.Disable();
    public void EnableInputs() => _gameInputs.Enable();

    public override void Stop() { _gameInputs.Disable(); _gameInputs = null; }
}