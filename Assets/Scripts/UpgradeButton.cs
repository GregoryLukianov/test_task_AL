using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonView: MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Button Button;

    public void Awake()
    {
        Text = GetComponentInChildren<TextMeshProUGUI>();
        Button = GetComponent<Button>();
    }
}