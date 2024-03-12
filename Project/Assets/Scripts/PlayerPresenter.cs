using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerPresenter : NetworkBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _sharedMoneyText;

    private TMP_Text _ownedMoneyText;

    private void OnEnable()
    {
        _player.Initialized += Initialize;
        _player.MoneyChanged += CallPresentMoneyServerRpc;
    }

    private void OnDisable()
    {
        _player.Initialized -= Initialize;
        _player.MoneyChanged -= CallPresentMoneyServerRpc;
    }

    private void Initialize(PlayerCanvas canvas, string name)
    {
        _ownedMoneyText = canvas.MoneyText;
        PresentNameServerRpc(name);
    }

    [ServerRpc]
    public void PresentNameServerRpc(string name)
    {
        PresentNameForOthersClientRpc(name);
    }

    [ClientRpc]
    private void PresentNameForOthersClientRpc(string name)
    {
        if(!IsOwner)
            _nameText.text = name;
    }

    [ServerRpc]
    private void CallPresentMoneyServerRpc(int money)
    {
        PresentMoneyClientRpc(money);
    }

    [ClientRpc]
    private void PresentMoneyClientRpc(int money)
    {
        if(IsOwner)
            _ownedMoneyText.text = money.ToString() + '$';
        else
            _sharedMoneyText.text = money.ToString() + '$';
    }
}