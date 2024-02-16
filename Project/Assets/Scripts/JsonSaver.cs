using System.IO;
using UnityEngine;

public class JsonSaver : MonoBehaviour
{
    public void Save<T>(string path, T data) where T : ISavable
    {
        path += ".json";
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
}
