using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayersSpawner : NetworkBehaviour
{
    [SerializeField] private Transform[] _playerPoints;
    [SerializeField] private Transform _lookPoint;
    [SerializeField] private PlayerCanvas _canvas;
    [SerializeField] private Player _player;

    public void SpawnPlayers(List<UserConnectionData> users)
    {
        Shuffle(_playerPoints);
        var pointIndex = 0;
        foreach(var user in users)
        {
            var player = Instantiate(_player, _playerPoints[pointIndex].position, _playerPoints[pointIndex].rotation).GetComponent<Player>();
            player.NetworkObject.SpawnAsPlayerObject(user.Id);
            InitialzePlayerClientRpc(user, pointIndex);
            pointIndex++;
        }
    }

    private void Shuffle(Transform[] points)
    {
        Transform current;
        int randIndex;
        for (int i = 0; i < points.Length; i++)
        {
            current = points[i];
            randIndex = Random.Range(0, points.Length);
            points[i] = points[randIndex];
            points[randIndex] = current;
        }
    }

    [ClientRpc]
    private void InitialzePlayerClientRpc(UserConnectionData user, int pointIndex)
    {
        if (NetworkManager.Singleton.LocalClientId == user.Id)
        {
            var player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Player>();
            player.Initialize(user, _canvas);
        }
    }
}
