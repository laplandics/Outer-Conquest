using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveLoader
{
    private static JsonSerializerSettings Settings => 
        new JsonSerializerSettings { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.All };
    private static string GetFile(string key) => Path.Combine(Application.persistentDataPath, $"{key}.json");
    
    public static void Save(string key, object data)
    {
        var json = JsonConvert.SerializeObject(data, Settings);
        File.WriteAllText(GetFile(key), json);
    }
    
    public static T Load<T>(string key)
    {
        var task = File.ReadAllText(GetFile(key));
        var data = JsonConvert.DeserializeObject<T>(task, Settings);
    
        return data;
    }
}