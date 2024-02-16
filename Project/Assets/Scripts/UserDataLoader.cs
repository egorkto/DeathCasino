using UnityEngine;

public class UserDataLoader : MonoBehaviour
{
    [SerializeField] private DataProvider _provider;
    [SerializeField] private UserDataPresenter _dataPresenter;

    private void Start()
    {
        var data = _provider.LoadUserData();
        _dataPresenter.PresentUser(data);
    }

    public UserData GetData()
    {
        return _provider.LoadUserData();
    }
}
