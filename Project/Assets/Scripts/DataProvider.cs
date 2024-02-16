using UnityEngine;

public class DataProvider : MonoBehaviour
{
    private const string userDataPath = "PlayerData";
    private const string defaultName = "Player";

    public void SaveUserData(UserData data)
    {
        var saver = new JsonSaver();
        saver.Save(Application.persistentDataPath + "/" + userDataPath, data);
    }

    public UserData LoadUserData()
    {
        var loader = new JsonLoader();
        var data = loader.TryLoad<UserData>(Application.persistentDataPath + "/" + userDataPath);
        if (data.Name == "")
            data.Name = defaultName;
        return data;
    }
}
