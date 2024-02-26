using Unity.Netcode;
using UnityEngine;

public class LobbyConnector : NetworkBehaviour
{
    [SerializeField] private Lobby _lobby;
    [SerializeField] private UserDataLoader _dataLoader;

    private void OnEnable()
    {
        User.Connected += ConnectUser;
    }

    private void OnDisable()
    {
        User.Connected -= ConnectUser;
    }

    private void ConnectUser(User user)
    {
        var userData = _dataLoader.GetData();
        var userLobbyData = new UserConnectionData()
        {
            Id = user.OwnerClientId,
            Name = userData.Name,
        };
        if (!_lobby.TryConnectUser(userLobbyData))
            NetworkManager.Singleton.DisconnectClient(user.OwnerClientId);
    }
}