using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerInteractionHandler : NetworkBehaviour
{
    public event Action TurnEnded;

    [SerializeField] private TurnsOrderer _orderer;
    [SerializeField] private PlayersSpawner _spawner;

    private Player _player;

    private void Start()
    {
        _player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (_orderer.TurningId == NetworkManager.Singleton.LocalClientId)
            {
                Debug.Log("Move");
                EndTurnServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void EndTurnServerRpc()
    {
        TurnEnded?.Invoke();
    }
}
