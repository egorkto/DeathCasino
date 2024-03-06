using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class TurnsOrderer : NetworkBehaviour
{
    public ulong TurningId { get; private set; }

    [SerializeField] private TurnPresenter _presenter;

    private Queue<ulong> _queue = new Queue<ulong>();

    public void InitialiizeQueue(List<UserConnectionData> users)
    {
        foreach (var user in users)
            _queue.Enqueue(user.Id);
        SetTurnNextPlayerServerRpc();
    }

    public int GetPlayersCount()
    {
        return _queue.Count;
    }

    public bool TryGetNextPlayerId(out ulong nextId)
    {
        if (_queue.TryPeek(out ulong id))
        {
            nextId = id;
            return true;
        }
        nextId = 0;
        return false;
    }

    [ServerRpc(RequireOwnership = false)]
    public void TryEndTurnServerRpc()
    {
        if(_queue.Count > 1)
            SetTurnNextPlayerServerRpc();
    }

    public void TryRemovePlayer(ulong id)
    {
        RemovePlayerServerRpc(id);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RemovePlayerServerRpc(ulong id)
    {
        var list = _queue.ToList();
        list.Remove(id);
        _queue = new Queue<ulong>(list);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetTurnNextPlayerServerRpc()
    {
        SetTurnIdClientRpc(_queue.Dequeue());
        _presenter.PresentTurn(TurningId);
        _queue.Enqueue(TurningId);
    }

    [ClientRpc]
    private void SetTurnIdClientRpc(ulong id)
    {
        TurningId = id;
    }
}
