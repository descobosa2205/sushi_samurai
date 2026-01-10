using System.Collections;
using UnityEngine;

public class FruitThiefAction : MonoBehaviour, ISamuraiAction
{
    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager;

    [Header("Trigger Condition")]
    [SerializeField] private int streakToTrigger = 10;
    [SerializeField] private float chancePerExecute = 0.6f;

    [Header("Effect")]
    [SerializeField] private int pointsToSteal = 10;
    [SerializeField] private float maxTargetDistance = 20f;

    [Header("Telegraph (Message)")]
    [SerializeField] private string message = "Te estás viniendo arriba... ¡esa es mía!";
    [SerializeField] private float telegraphSeconds = 0.6f;

    [Header("Cooldown")]
    [SerializeField] private float cooldownSeconds = 6f;
    public float CooldownSeconds => cooldownSeconds;

    private bool isRunning;

    public bool CanExecute(IPerformanceMetrics metrics)
    {
        if (isRunning) return false;
        if (gameManager == null) return false;
        if (metrics.CurrentStreak < streakToTrigger) return false;
        return true;
    }

    public void Execute()
    {
        if (isRunning) return;
        if (Random.value > chancePerExecute) return;

        Sushi target = FindTargetSushi();
        if (target == null) return;

        StartCoroutine(StealRoutine(target));
    }

    private IEnumerator StealRoutine(Sushi target)
    {
        isRunning = true;

        SamuraiMessageBus.Publish(message);

        if (telegraphSeconds > 0f)
            yield return new WaitForSeconds(telegraphSeconds);

        // Si el target ya no existe (lo cortó el jugador o se destruyó), cancelamos
        if (target == null)
        {
            isRunning = false;
            yield break;
        }

        SamuraiContext.LastSamuraiSliceFrame = Time.frameCount;

        Vector3 dir = (target.transform.position - transform.position).normalized;
        if (dir == Vector3.zero) dir = Vector3.right;

        target.Slice(dir, target.transform.position, 1f);

        // Neto -10 puntos (Slice suma +1, así que restamos 11)
        gameManager.AddScore(-(pointsToSteal + 1));

        isRunning = false;
    }

    private Sushi FindTargetSushi()
    {
        Sushi[] all = Object.FindObjectsByType<Sushi>(FindObjectsSortMode.None);

        Sushi best = null;
        float bestDist = float.MaxValue;

        for (int i = 0; i < all.Length; i++)
        {
            Sushi s = all[i];
            if (s == null) continue;

            float d = Vector3.Distance(transform.position, s.transform.position);
            if (d > maxTargetDistance) continue;

            if (d < bestDist)
            {
                bestDist = d;
                best = s;
            }
        }

        return best;
    }
}
