using UnityEngine;
using System;
using System.Collections;

public class SpecialSushi : Sushi
{
    private bool teleported;
    [SerializeField] private ParticleSystem sauceParticle;

    private void Awake()
    {
        teleported = false;
    }

    private void Teleport()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-2f, 2f), 0f);
    }

    public void TeleportedSlice(Vector3 direction, Vector3 position, float force)
    {
        if (!teleported)
        {
            sauceParticle.Play();
            Teleport();
            teleported = true;
        }
        else
        {
            Slice(direction, position, force);
            OnSushiPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            TeleportedSlice(blade.direction, blade.transform.position, blade.sliceForce);
        }
        else if (other.CompareTag("Finish"))
        {
            OnSushiMiss();
            Destroy(gameObject);
        }
    }
}

