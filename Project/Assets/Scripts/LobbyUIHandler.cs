using System;
using Unity.Netcode;
using UnityEngine;

public class LobbyUIHandler : NetworkBehaviour
{
    [SerializeField] private Lobby _lobby;
 
    public void DisconnectClient()
    {
        _lobby.TryDisconnectUser(NetworkManager.Singleton.LocalClientId);
    }

    public void StopHost()
    {
        _lobby.StopHost();
    }

    public void StartGame()
    {
        _lobby.StartGameServerRpc();
    }

    public void SetReadiness(bool value)
    {
        _lobby.TrySetReadinessServerRpc(value, NetworkManager.Singleton.LocalClientId);
    }
}
