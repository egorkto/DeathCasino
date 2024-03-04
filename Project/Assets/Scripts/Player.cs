using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public static event Action<ulong> Losed;
    public static event Action<Player> Spawned;

    public event Action<PlayerCanvas, string> Initialized;
    public event Action<int> MoneyChanged;

    [SerializeField] private int _startMoneyCount;
    [SerializeField] private Transform _cameraPoint;

    private int _currentMoney;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
            Spawned?.Invoke(this);
    }

    public void Initialize(UserConnectionData user, PlayerCanvas canvas)
    {
        _currentMoney = _startMoneyCount;
        Initialized?.Invoke(canvas, user.Name.ToString());
        MoneyChanged?.Invoke(_currentMoney);
        Camera.main.transform.SetParent(_cameraPoint);
        Camera.main.transform.position = _cameraPoint.position;
    }
    
    public void IncreaseMoney(int value)
    {
        _currentMoney += value;
        MoneyChanged?.Invoke(_currentMoney);
    }

    public void TryDecreaseMoney(int value)
    {
        if (_currentMoney > value)
        {
            _currentMoney -= value;
        }
        else
        {
            _currentMoney = 0;
            Losed?.Invoke(OwnerClientId);
        }
        MoneyChanged?.Invoke(_currentMoney);
    }

    public void MultiplyMoney(float value)
    {
        _currentMoney = (int)(_currentMoney * value);
        MoneyChanged?.Invoke(_currentMoney);
    }
}
