using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class Lobby : NetworkBehaviour
{
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private LobbyPresenter _presenter;
    [SerializeField] private UnityTransport _transport;
    [SerializeField] private byte _maxUsers;
    [SerializeField] private byte _startGameUsersLimit;

    private Dictionary<UserConnectionData, bool> _users = new Dictionary<UserConnectionData, bool>();

    public bool TryConnectUser(UserConnectionData data)
    {
        if(_users.Count <= _maxUsers)
        {
            _presenter.InitializeLobbyWindow(_transport.ConnectionData.Address, _maxUsers, IsHost);
            AddUserServerRpc(data, IsHost);
            return true;
        }
        return false;
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddUserServerRpc(UserConnectionData data, bool isHost)
    {
        _users[data] = isHost;
        TrySetCanStartGame();
        _presenter.PresentUsers(_users);
    }

    private void TrySetCanStartGame()
    {
        _presenter.PresentCanStartGame(_users.Count >= _startGameUsersLimit && !_users.ContainsValue(false));
    }

    [ServerRpc(RequireOwnership = false)]
    public void StartGameServerRpc()
    {
        _gameStarter.StartGame(_users.Keys.ToList());
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
            if(user.Key.Id == id)
            {
                _users.Remove(user.Key);
                TrySetCanStartGame();
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
        _users = new Dictionary<UserConnectionData, bool>();
    }

    [ServerRpc(RequireOwnership = false)]
    public void TrySetReadinessServerRpc(bool value, ulong id)
    {
        foreach (var user in _users)
        {
            if (user.Key.Id == id)
            {
                _users[user.Key] = value;
                TrySetCanStartGame();
                _presenter.PresentUsers(_users);
                return;
            }
        }
    }
}
