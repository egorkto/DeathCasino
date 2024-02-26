using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayersSpawner : NetworkBehaviour
{
    [SerializeField] private Transform[] _playerPoints;
    [SerializeField] private Player _player;

    public List<Player> SpawnPlayers(List<UserConnectionData> users)
    {
        var players = new List<Player>();
        foreach(var user in users)
        {
            var player = Instantiate(_player).GetComponent<Player>();
            player.Initialize(user.Name.ToString(), user.Id);
            players.Add(player);
        }
        return players;
    }
}
