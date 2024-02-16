using System;
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

    private void ConnectUser(NetworkBehaviour networkObject)
    {
        var userData = _dataLoader.GetData();
        var userLobbyData = new UserLobbyData()
        {
            Id = networkObject.OwnerClientId,
            Name = userData.Name,
            Ready = networkObject.IsServer
        };
        if (!_lobby.TryConnectUser(userLobbyData))
            NetworkManager.Singleton.DisconnectClient(networkObject.OwnerClientId);
    }
}