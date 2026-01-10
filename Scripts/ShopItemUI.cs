using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Header("Config")]
    public BackgroundType background;

    [Header("References")]
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button button;

    private void Awake()
    {
        if (buttonText == null)
            buttonText = GetComponentInChildren<TMP_Text>(true);

        if (button == null)
            button = GetComponent<Button>();
    }

    public void Refresh()
    {
        if (buttonText == null) return;

        // 1) Si es el seleccionado actualmente
        if (PlayerData.SelectedBackground == background)
        {
            buttonText.text = "Currently selected";
            // Opcional: bloquear click si ya está seleccionado
            if (button != null) button.interactable = false;
            return;
        }

        // Si NO está seleccionado, permitimos click
        if (button != null) button.interactable = true;

        // 2) Si no está comprado -> mostrar precio
        bool bought = PlayerData.IsBought(background); // Wood devuelve true siempre
        if (!bought)
        {
            int price = PlayerData.GetPrice(background);
            buttonText.text = $"Buy for {price} coins";
            return;
        }

        // 3) Comprado (o Wood) pero no seleccionado
        buttonText.text = "Select";
    }
}