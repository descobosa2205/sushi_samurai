using System.Collections;
using UnityEngine;

public class ScreenBlockAction : MonoBehaviour, ISamuraiAction
{
    [Header("Dependencies")]
    [SerializeField] private Renderer[] renderers; // MeshRenderer / SkinnedMeshRenderer

    [Header("Trigger Condition")]
    [SerializeField] private int streakToTrigger = 8;

    [Header("Effect")]
    [SerializeField] private float durationSeconds = 2.0f;

    [SerializeField] private Vector2 xRange = new Vector2(-4f, 4f);
    [SerializeField] private Vector2 yRange = new Vector2(-2f, 2f);
    [SerializeField] private Vector3 blockScale = new Vector3(2.5f, 2.5f, 2.5f);
    [SerializeField] private float zPosition = -2f;


    [Header("Cooldown")]
    [SerializeField] private float cooldownSeconds = 10f;
    public float CooldownSeconds => cooldownSeconds;

    private Coroutine routine;

    private void Reset()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    public bool CanExecute(IPerformanceMetrics metrics)
    {
        if (renderers == null || renderers.Length == 0) return false;
        return metrics.CurrentStreak >= streakToTrigger;
    }

    public void Execute()
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(BlockRoutine());
    }

    private IEnumerator BlockRoutine()
    {
        SamuraiMessageBus.Publish("Te estoy vigilando...");
        float x = Random.Range(xRange.x, xRange.y);
        float y = Random.Range(yRange.x, yRange.y);
        transform.localScale = blockScale;
        transform.position = new Vector3(x, y, zPosition);

        SetVisible(true);
        yield return new WaitForSeconds(durationSeconds);
        SetVisible(false);

        routine = null;
    }

    private void SetVisible(bool visible)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null) renderers[i].enabled = visible;
        }
    }
}
