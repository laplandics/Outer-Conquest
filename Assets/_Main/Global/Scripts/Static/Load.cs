using UnityEngine;

namespace G
{
    public static class Load
    {
        public static T LoadConfig<T>(string name) where T : Config
        {
            var path = $"Config/{name}";
            var config = (T)Resources.Load(path);
            if (config == null) UnityEngine.Debug.LogError($"Config {name} not found in {path}");
            return config;
        }
        
        public static Config LoadConfig(string name)
        {
            var path = $"Config/{name}";
            var config = (Config)Resources.Load(path);
            if (config == null) UnityEngine.Debug.LogError($"Config {name} not found in {path}");
            return config;
        }
    }
}