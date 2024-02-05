using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button _host, _server, _client;

    private void Awake()
    {
        _host.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        _server.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
        _client.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
    }
}
