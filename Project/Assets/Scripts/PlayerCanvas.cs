using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    public TMP_Text MoneyText => _moneyText;

    [SerializeField] private TMP_Text _moneyText;
}
