using System;
using Unity.Netcode;
using UnityEngine;

public class User : NetworkBehaviour
{
    public static event Action<NetworkBehaviour> Connected;

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += TryConnect;
    }

    public override void OnNetworkDespawn()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= TryConnect;
    }

    private void TryConnect(ulong id)
    {
        if (IsOwner && id == OwnerClientId)
        {

            Connected?.Invoke(this);
        }
    }
}
