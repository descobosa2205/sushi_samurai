using UnityEngine;
using System;

public class Sushi : MonoBehaviour, IThrowable
{
    [SerializeField] private GameObject whole;
    [SerializeField] private GameObject sliced;
    [SerializeField] private ParticleSystem juiceParticle;
    [SerializeField] private AudioClip sushiCut;

    private AudioSource sushiSpeaker;

    private Collider sushiCollider;
    private Rigidbody sushiRigidbody;

    public static event Action SushiPoint;
    public static event Action SushiMiss;

    private void Awake()
    {
        sushiRigidbody = GetComponent<Rigidbody>();
        sushiCollider = GetComponent<Collider>();
        sushiSpeaker = GetComponent<AudioSource>();
    }

    protected virtual void OnSushiPoint()
    {
        SushiPoint?.Invoke();
    }

    protected virtual void OnSushiMiss()
    {
        SushiMiss?.Invoke();
    }

    public void Slice(Vector3 direction, Vector3 position, float force)
    {
        juiceParticle.Play();

        sushiSpeaker = GetComponent<AudioSource>();
        sushiSpeaker.PlayOneShot(sushiCut);

        whole.SetActive(false);
        sliced.SetActive(true);

        sushiCollider = GetComponent<Collider>();
        sushiCollider.enabled = false;

        SushiPoint?.Invoke();

        float angle = Mathf.Atan2(direction.y, direction.x);
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        sushiRigidbody = GetComponent<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.linearVelocity = sushiRigidbody.linearVelocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
        else if (other.CompareTag("Finish"))
        {
            SushiMiss?.Invoke();
            Destroy(gameObject);
        }
    }
}
