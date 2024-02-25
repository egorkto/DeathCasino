using Unity.Netcode;
using UnityEngine;

public class GameInitializer : NetworkBehaviour
{
    [SerializeField] private PlayersSpawner _spawner;
    [SerializeField] private TurnsOrderer _orderer;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            var players = _spawner.SpawnPlayers(UsersHolder.GetUsers());
            _orderer.SetPlayers(players);
        }
    }

    private void Start()
    {

    }
}
