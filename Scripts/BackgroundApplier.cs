using UnityEngine;

public class BackgroundApplier : MonoBehaviour
{
    [Header("Assign the background GameObjects in the scene")]
    public GameObject wood;
    public GameObject rio;
    public GameObject cocina;
    public GameObject terraza;

    private void Start()
    {
        Apply(PlayerData.SelectedBackground);
    }

    public void Apply(BackgroundType bg)
    {
        // Si no hay ninguno definido inicialmente -> Wood por defecto
        if (wood == null && rio == null && cocina == null && terraza == null)
            return;

        // Apaga todos
        if (wood) wood.SetActive(false);
        if (rio) rio.SetActive(false);
        if (cocina) cocina.SetActive(false);
        if (terraza) terraza.SetActive(false);

        // Enciende el elegido (si falta, cae a wood)
        switch (bg)
        {
            case BackgroundType.Rio:
                if (rio) rio.SetActive(true);
                else if (wood) wood.SetActive(true);
                break;

            case BackgroundType.Cocina:
                if (cocina) cocina.SetActive(true);
                else if (wood) wood.SetActive(true);
                break;

            case BackgroundType.Terraza:
                if (terraza) terraza.SetActive(true);
                else if (wood) wood.SetActive(true);
                break;

            default:
                if (wood) wood.SetActive(true);
                break;
        }
    }
}