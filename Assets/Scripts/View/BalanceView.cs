using UnityEngine;
using TMPro;

public class BalanceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;

    public void SetBalance(float value)
    {
        _balanceText.text = $"Баланс: {value:F0}$";
    }
}