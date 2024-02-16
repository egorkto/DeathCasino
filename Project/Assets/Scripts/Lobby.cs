using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class Lobby : NetworkBehaviour
{
    [SerializeField] private LobbyPresenter _presenter;
    [SerializeField] private UnityTransport _transport;
    [SerializeField] private byte _maxUsers;
    [SerializeField] private byte _startGameUsersLimit;

    private List<UserLobbyData> _users = new List<UserLobbyData>();

    public bool TryConnectUser(UserLobbyData data)
    {
        if(_users.Count <= _maxUsers)
        {
            _presenter.InitializeLobbyWindow(_transport.ConnectionData.Address, _maxUsers, IsServer);
            AddUserServerRpc(data);
            return true;
        }
        return false;
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddUserServerRpc(UserLobbyData data)
    {
        _users.Add(data);
        if (_users.Count >= _startGameUsersLimit && AllUsersReady(_users))
            _presenter.PresentCanStartGame(true);
        else
            _presenter.PresentCanStartGame(false);
        _presenter.PresentUsers(_users);
    }

    [ServerRpc]
    private void TrySetCanStartGameServerRpc()
    {
        if (_users.Count >= _startGameUsersLimit && AllUsersReady(_users))
            _presenter.PresentCanStartGame(true);
        else
            _presenter.PresentCanStartGame(false);
    }

    private bool AllUsersReady(List<UserLobbyData> users)
    {
        foreach (var user in users)
            if (!user.Ready)
                return false;
        return true;
    }

    [ServerRpc]
    public void StartGameServerRpc()
    {
        Debug.Log("Game Started");
        _presenter.CloseLobbyClientRpc();
    }

    public void TryDisconnectUser(ulong id)
    {
        TryRemoveUserServerRpc(id);
        _presenter.CloseLobby();
        NetworkManager.Singleton.Shutdown();
    }

    [ServerRpc(RequireOwnership = false)]
    private void TryRemoveUserServerRpc(ulong id)
    {
        foreach(var user in _users)
        {
            if(user.Id == id)
            {
                _users.Remove(user);
                TrySetCanStartGameServerRpc();
                _presenter.PresentUsers(_users);
                return;
            }
        }
    }

    public void StopHost()
    {
        _presenter.CloseLobbyClientRpc();
        ClearUsersServerRpc();
        NetworkManager.Singleton.Shutdown();
    }

    [ServerRpc]
    private void ClearUsersServerRpc()
    {
        _users = new List<UserLobbyData>();
    }

    [ServerRpc(RequireOwnership = false)]
    public void TrySetReadinessServerRpc(bool value, ulong id)
    {
        for(var i = 0; i < _users.Count; i++)
        {
            if(_users[i].Id == id)
            {
                _users[i] = new UserLobbyData()
                {
                    Id = _users[i].Id,
                    Name = _users[i].Name,
                    Ready = value
                };
                TrySetCanStartGameServerRpc();
                _presenter.PresentUsers(_users);
                return;
            }
        }
    }
}
