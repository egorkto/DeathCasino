using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : NetworkBehaviour
{
    [SerializeField] private TurnsOrderer _orderer;
    [SerializeField] private EndGamePresenter _presenter;
    [SerializeField] private string _menuSceneName;

    private void OnEnable()
    {
        Player.Losed += EndGameServerRpc;
    }

    private void OnDisable()
    {
        Player.Losed -= EndGameServerRpc;
    }

    [ServerRpc(RequireOwnership = false)]
    private void WinGameServerRpc()
    {
        EndGameClientRpc(_orderer.TurningId, true);
    }

    [ServerRpc(RequireOwnership = false)]
    private void EndGameServerRpc(ulong id)
    {
        _orderer.TryRemovePlayer(id);
        EndGameClientRpc(id, false);
        if (_orderer.GetPlayersCount() == 1 && _orderer.TryGetNextPlayerId(out ulong nextId))
            EndGameClientRpc(nextId, true);
    }

    [ClientRpc]
    private void EndGameClientRpc(ulong id, bool win)
    {
        if (NetworkManager.Singleton.LocalClientId == id)
        {
            StartCoroutine(EndGame(win));
        }
    }

    private IEnumerator EndGame(bool win)
    {
        yield return StartCoroutine(_presenter.PresentResult(win));
        NetworkManager.Singleton.Shutdown();
        Destroy(NetworkManager.Singleton.gameObject);
        SceneManager.LoadScene(_menuSceneName, LoadSceneMode.Single);
    }
}
