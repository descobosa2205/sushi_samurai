using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Fruit_Ninja");
    }

    public void IrAMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}