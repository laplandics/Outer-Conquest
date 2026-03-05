using UnityEngine;

public class UiEmpty : MonoBehaviour
{
    public void Initialize()
    { gameObject.name = "UI"; DontDestroyOnLoad(gameObject); }
}