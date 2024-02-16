using TMPro;
using UnityEngine;

public class UserDataPresenter : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInputField;

    public void PresentUser(UserData data)
    {
        _nameInputField.text = data.Name;
    }
}