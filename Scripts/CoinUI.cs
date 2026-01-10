using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        if (coinText == null)
            coinText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        if (coinText == null)
        {
            Debug.LogWarning("CoinUI: coinText no está asignado y no hay TextMeshProUGUI en el mismo objeto.");
            return;
        }

        coinText.text = PlayerData.Coins.ToString();
    }
}