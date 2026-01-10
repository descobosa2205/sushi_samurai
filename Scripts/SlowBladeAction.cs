using System.Collections;
using UnityEngine;

public class SlowBladeAction : MonoBehaviour, ISamuraiAction
{
    [Header("Dependencies")]
    [SerializeField] private Blade blade;

    [Header("Trigger Condition")]
    [SerializeField] private int streakToTrigger = 12;

    [Header("Effect")]
    [SerializeField] private float durationSeconds = 4f;
    [SerializeField] private float minVelocityMultiplier = 8f; // subir minVelocity = más difícil cortar
    [SerializeField] private float sliceForceMultiplier = 0.8f;
    [SerializeField] private float followSpeedMultiplier = 0.2f; // MUY notable


    [Header("Cooldown")]
    [SerializeField] private float cooldownSeconds = 10f;
    public float CooldownSeconds => cooldownSeconds;

    private Coroutine routine;

    public bool CanExecute(IPerformanceMetrics metrics)
    {
        if (blade == null) return false;
        return metrics.CurrentStreak >= streakToTrigger;
    }

    public void Execute()
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(ApplyDebuff());
    }

    private IEnumerator ApplyDebuff()
    {
        SamuraiMessageBus.Publish("¡Cuidado! No vayas tan rápido...");

        float originalMinVel = blade.minVelocity;
        float originalForce = blade.sliceForce;

        blade.SetFollowSpeedMultiplier(followSpeedMultiplier);
        blade.minVelocity = originalMinVel * minVelocityMultiplier;
        blade.sliceForce = originalForce * sliceForceMultiplier;
        blade.SetFollowSpeedMultiplier(1f);

        yield return new WaitForSeconds(durationSeconds);

        // restaurar
        if (blade != null)
        {
            blade.minVelocity = originalMinVel;
            blade.sliceForce = originalForce;
        }

        routine = null;
    }
}
