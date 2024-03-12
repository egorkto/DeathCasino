using Unity.Netcode;
using UnityEngine;

public class GameInitializer : NetworkBehaviour
{
    [SerializeField] private PlayersSpawner _spawner;
    [SerializeField] private TurnsOrderer _orderer;

    private void OnEnable()
    {
        GameStarter.GameStarted += TryInitializeGame;
    }

    private void OnDisable()
    {
        GameStarter.GameStarted -= TryInitializeGame;
    }

    private void TryInitializeGame()
    {
        if (IsServer)
        {
            _spawner.SpawnPlayers(UsersHolder.GetUsers());
            _orderer.InitialiizeQueue(UsersHolder.GetUsers());
        }
    }
}
