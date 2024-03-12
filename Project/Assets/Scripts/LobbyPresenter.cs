using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPresenter : NetworkBehaviour
{
    [SerializeField] private GameObject _lobbyWindow;
    [SerializeField] private TMP_Text _usersCountText;
    [SerializeField] private TMP_Text _ipText;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private GameObject _hostButtons;
    [SerializeField] private GameObject _clientButtons;
    [SerializeField] private GameObject _usersParent;
    [SerializeField] private UserLobbyPresent _userLobbyPresent;

    private List<UserLobbyPresent> _users = new List<UserLobbyPresent>();

    private byte _maxUsers;

    public void InitializeLobbyWindow(string ip, byte maxUsers, bool isHost)
    {
        _lobbyWindow.SetActive(true);
        _ipText.text = ip;
        _maxUsers = maxUsers;
        _usersCountText.text = _users.Count + "/" + _maxUsers;
        _hostButtons.SetActive(isHost);
        _clientButtons.SetActive(!isHost);
    }

    public void PresentCanStartGame(bool state)
    {
        _startGameButton.gameObject.SetActive(state);
    }

    public void PresentUsers(Dictionary<UserConnectionData, bool> users)
    {
        ClearUsersClientRpc();
        foreach(var user in users)
        {
            PresentUserClientRpc(user.Key, user.Value);
        }
    }

    [ClientRpc]
    private void PresentUserClientRpc(UserConnectionData data, bool ready)
    {
        var present = Instantiate(_userLobbyPresent, _usersParent.transform).GetComponent<UserLobbyPresent>();
        present.SetName(data.Name);
        present.SetReady(ready);
        _users.Add(present);
        _usersCountText.text = _users.Count + "/" + _maxUsers;
    }

    [ClientRpc]
    private void ClearUsersClientRpc()
    {
        foreach (var user in _users)
            Destroy(user.gameObject);
        _users = new List<UserLobbyPresent>();
    }

    public void CloseLobby()
    {
        _lobbyWindow.SetActive(false);
    }

    [ClientRpc]
    public void CloseLobbyClientRpc()
    {
        _lobbyWindow.SetActive(false);
    }
}
