using TMPro;
using UnityEngine;

public class UserDataSaver : MonoBehaviour
{
    [SerializeField] private DataProvider _provider;
    [SerializeField] private TMP_Text _nameText;

    public void SaveData()
    {
        var data = new UserData()
        {
            Name = _nameText.text
        };
        _provider.SaveUserData(data);
    }
}
