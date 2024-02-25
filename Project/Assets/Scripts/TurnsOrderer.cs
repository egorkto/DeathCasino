using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TurnsOrderer : NetworkBehaviour
{
    public ulong TurningId { get; private set; }

    [SerializeField] private PlayerInteractionHandler _interactionHandler;
    [SerializeField] private TurnPresenter _presenter;

    private Queue<ulong> _ids = new Queue<ulong>();

    public void SetPlayers(List<Player> players)
    {
        foreach (var player in players)
            _ids.Enqueue(player.OwnerClientId);
        SetTurnNextPlayerServerRpc();
    }

    public void EndTurn()
    {
        SetTurnNextPlayerServerRpc();
    }

    private void OnEnable()
    {
        _interactionHandler.TurnEnded += SetTurnNextPlayerServerRpc;
    }

    private void OnDisable()
    {
        _interactionHandler.TurnEnded -= SetTurnNextPlayerServerRpc;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetTurnNextPlayerServerRpc()
    {
        SetTurnIdClientRpc(_ids.Dequeue());
        _presenter.PresentTurn(TurningId);
        _ids.Enqueue(TurningId);
    }

    [ClientRpc]
    private void SetTurnIdClientRpc(ulong id)
    {
        TurningId = id;
        Debug.Log("id: " + id);
    }
}
