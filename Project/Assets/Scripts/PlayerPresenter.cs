using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerPresenter : NetworkBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _moneyText;

    private void OnEnable()
    {
        _player.Initialized += PresentNameClientRpc;
        _player.MoneyChanged += PresentMoneyClientRpc;
    }

    private void OnDisable()
    {
        _player.Initialized -= PresentNameClientRpc;
        _player.MoneyChanged -= PresentMoneyClientRpc;
    }

    [ClientRpc]
    public void PresentNameClientRpc(string name)
    {
        _nameText.text = name;
    }

    [ClientRpc]
    private void PresentMoneyClientRpc(int money)
    {
        _moneyText.text = money.ToString() + '$';
    }
}