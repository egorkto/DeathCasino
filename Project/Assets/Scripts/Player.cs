using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public event Action<string> Initialized;
    public event Action<int> MoneyChanged;

    public bool Moving => _moving.Value;

    [SerializeField] private NetworkObject _networkObject;
    [SerializeField] private int _startMoneyCount;

    private NetworkVariable<bool> _moving = new NetworkVariable<bool>();
    private int _currentMoney;

    public void Initialize(string name, ulong id)
    {
        _currentMoney = _startMoneyCount;
        _networkObject.SpawnAsPlayerObject(id);
        Initialized?.Invoke(name);
        MoneyChanged?.Invoke(_currentMoney);
    }

    public void SetTurn(bool value)
    {
        if(IsOwner)
            _moving.Value = value;
    }
}
