using UnityEngine;

public class CoroutineEmpty : MonoBehaviour
{
    public void Initialize()
    { gameObject.name = "Coroutine"; DontDestroyOnLoad(gameObject); }
}