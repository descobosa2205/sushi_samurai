using UnityEngine;
using System;

public class Bomb : MonoBehaviour, IThrowable
{
    [SerializeField] private ParticleSystem explosionParticle;

    [SerializeField] private AudioSource bombSpeaker;

    public static event Action BombTriggered;

    private void Explode()
    {
        explosionParticle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bombSpeaker.transform.SetParent(null);
            explosionParticle.transform.SetParent(null);
            Explode();
            Destroy(explosionParticle.gameObject, 1.0f);
            Destroy(bombSpeaker.gameObject, 1.0f);
            Destroy(gameObject);
            BombTriggered?.Invoke();
        }
    }
}
