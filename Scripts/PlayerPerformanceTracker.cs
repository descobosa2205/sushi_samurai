using UnityEngine;

public class PlayerPerformanceTracker : MonoBehaviour, IPerformanceMetrics
{
    [SerializeField] private int currentStreak;
    [SerializeField] private float secondsSinceLastMiss;

    public int CurrentStreak => currentStreak;
    public float SecondsSinceLastMiss => secondsSinceLastMiss;

    private void OnEnable()
    {
        Sushi.SushiPoint += OnSushiPoint;
        Sushi.SushiMiss += OnSushiMiss;
        secondsSinceLastMiss = 0f;
        currentStreak = 0;
    }

    private void OnDisable()
    {
        Sushi.SushiPoint -= OnSushiPoint;
        Sushi.SushiMiss -= OnSushiMiss;
    }

    private void Update()
    {
        secondsSinceLastMiss += Time.deltaTime;
    }

    private void OnSushiPoint()
    {
        // Si el punto lo generó el samurái, lo ignoramos para no “falsear” la racha
        if (Time.frameCount == SamuraiContext.LastSamuraiSliceFrame) return;

        currentStreak++;
    }

    private void OnSushiMiss()
    {
        currentStreak = 0;
        secondsSinceLastMiss = 0f;
    }
}


