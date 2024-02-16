using System.IO;
using UnityEngine;

public class JsonLoader
{
    public T TryLoad<T>(string path) where T : ISavable
    {
        path += ".json";
        if (File.Exists(path))
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        return default(T);
    }
}