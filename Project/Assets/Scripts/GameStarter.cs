using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : NetworkBehaviour
{
    public static event Action GameStarted;

    [SerializeField] private string _gameSceneName; 
    
    private NetworkVariable<byte> _loadedClients = new NetworkVariable<byte>();

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.SceneManager.OnLoadComplete += AddLoadedClient;
    }

    public override void OnNetworkDespawn()
    {
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= AddLoadedClient;
    }

    private void AddLoadedClient(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId)
            AddLoadedClientServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddLoadedClientServerRpc()
    {
        _loadedClients.Value += 1;
    }

    public void StartGame(List<UserConnectionData> users)
    {
        UsersHolder.SetUsers(users);
        StartCoroutine(LoadGameScene((byte)users.Count));
    }

    private IEnumerator LoadGameScene(byte count)
    {
        var state = NetworkManager.Singleton.SceneManager.LoadScene(_gameSceneName, LoadSceneMode.Single);
        yield return new WaitUntil(() => _loadedClients.Value == count);
        GameStarted?.Invoke();
        DestroyClientRpc();
    }

    [ClientRpc]
    private void DestroyClientRpc()
    {
        Destroy(gameObject);
    }
}
