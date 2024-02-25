using System;
using Unity.Netcode;
using UnityEngine;

public class User : NetworkBehaviour
{
    public static event Action<User> Spawned;
    public static event Action<User> Connected;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
            Spawned?.Invoke(this);
        NetworkManager.Singleton.OnClientConnectedCallback += TryConnect;
    }

    public override void OnNetworkDespawn()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= TryConnect;
    }

    private void TryConnect(ulong id)
    {
        if (IsOwner && id == OwnerClientId)
            Connected?.Invoke(this);
    }
}
