using System.Collections.Generic;
using UnityEngine;

public class SamuraiDirector : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerPerformanceTracker tracker;

    [Header("Actions")]
    [SerializeField] private SlowBladeAction slowBladeAction;
    [SerializeField] private ScreenBlockAction screenBlockAction; // o ScreenBlockAction si usas el 2D
    [SerializeField] private FruitThiefAction fruitThiefAction;


    [Header("AI Tuning")]
    [SerializeField] private float thinkInterval = 0.5f;

    private readonly Dictionary<ISamuraiAction, float> nextAllowedTime = new();
    private List<ISamuraiAction> actions;

    private float nextThink;

    private void Awake()
    {
        // Convertimos MonoBehaviour -> ISamuraiAction (sin acoplar a clases concretas)
        actions = new List<ISamuraiAction>(3);

        AddActionIfValid(slowBladeAction);
        AddActionIfValid(screenBlockAction);
        AddActionIfValid(fruitThiefAction);
    }

    private void AddActionIfValid(ISamuraiAction act)
    {
        if (act == null) return;
        actions.Add(act);
        nextAllowedTime[act] = 0f;
    }

    private void Update()
    {
        if (tracker == null || actions == null || actions.Count == 0) return;

        if (Time.time < nextThink) return;
        nextThink = Time.time + thinkInterval;

        // Elegimos 1 acción ejecutable por ciclo (para no spamear)
        for (int i = 0; i < actions.Count; i++)
        {
            var action = actions[i];

            if (Time.time < nextAllowedTime[action]) continue;
            if (!action.CanExecute(tracker)) continue;

            action.Execute();
            nextAllowedTime[action] = Time.time + action.CooldownSeconds;
            break;
        }
    }
}
