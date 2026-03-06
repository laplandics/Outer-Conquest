using System.Collections;
using UnityEngine.SceneManagement;

namespace G
{
    public static class Scene
    {
        public const string BOOT = "Boot";
        public const string GAME = "Game";
        
        public static IEnumerator LoadScene(string sceneName) {
            yield return SceneManager.LoadSceneAsync("Boot"); yield return null;
            yield return SceneManager.LoadSceneAsync(sceneName); yield return null; }
    }
}