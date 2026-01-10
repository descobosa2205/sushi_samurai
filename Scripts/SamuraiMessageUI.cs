using System.Collections;
using TMPro;
using UnityEngine;

public class SamuraiMessageUI : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private float showSeconds = 1.5f;

    private Coroutine routine;

    private void Awake()
    {
        if (messageText == null)
            messageText = GetComponent<TMP_Text>();

        if (messageText != null)
        {
            messageText.text = "";
            var c = messageText.color;
            c.a = 0f;
            messageText.color = c;
        }
    }

    private void OnEnable()
    {
        SamuraiMessageBus.OnMessage += Show;
    }

    private void OnDisable()
    {
        SamuraiMessageBus.OnMessage -= Show;
    }

    private void Show(string msg)
    {
        if (messageText == null) return;

        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(ShowRoutine(msg));
    }

    private IEnumerator ShowRoutine(string msg)
    {
        messageText.text = msg;

        // Mostrar
        var c = messageText.color;
        c.a = 1f;
        messageText.color = c;

        yield return new WaitForSeconds(showSeconds);

        // Ocultar 
        c.a = 0f;
        messageText.color = c;

        routine = null;
    }
}
