using TMPro;
using Unity.Collections;
using UnityEngine;

public class UserLobbyPresent : MonoBehaviour
{
    public string Name => _nameText.text;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _readyText;
    [SerializeField] private string _readyInscription;
    [SerializeField] private string _unreadyInscription;
    [SerializeField] private Color _readyTextColor;
    [SerializeField] private Color _unreadyTextColor;

    public void SetName(FixedString128Bytes name)
    {
        _nameText.text = name.ToString();
    }

    public void SetReady(bool value)
    {
        _readyText.text = value ? _readyInscription : _unreadyInscription;
        _readyText.color = value ? Color.green : Color.white;
    }
}