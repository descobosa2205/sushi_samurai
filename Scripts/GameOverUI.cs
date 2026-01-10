using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Awake()
    {
        if (pointsText == null)
            pointsText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdatePoints();
    }

    public void UpdatePoints()
    {
        if (pointsText == null) return;

        // Muestra los puntos de la última partida
        pointsText.text = PlayerData.LastRunPoints.ToString();
    }
}