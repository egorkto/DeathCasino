using UnityEngine;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net;
using System.Net.Sockets;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{
	[SerializeField] private UnityTransport _transport;
	[SerializeField] private TMP_InputField _ipInputField;

	private string _currentIP;

	public void StartHost()
	{
		GetLocalIPAddress();
        NetworkManager.Singleton.StartHost();
	}

	public void StartClient()
	{
		_currentIP = _ipInputField.text;
		_transport.ConnectionData.Address = _currentIP;
		NetworkManager.Singleton.StartClient();
	}

	public void GetLocalIPAddress()
	{
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				_currentIP = ip.ToString();
				_transport.ConnectionData.Address = _currentIP;
				return;
			}
		}
		throw new System.Exception("No network adapters with an IPv4 address in the system!");
	}

	private void Start()
	{
		_currentIP = "0.0.0.0";
		_transport.ConnectionData.Address = _currentIP;
	}
}
