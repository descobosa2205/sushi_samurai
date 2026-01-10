using UnityEngine;
using UnityEngine.InputSystem;

public class Blade : MonoBehaviour
{
    private bool slice;
    private Collider bladeCollider;
    private Camera mainCamera;

    public Vector3 direction { get; private set; }
    public float sliceForce = 2f;
    public float minVelocity = 0.01f;
    public float followSpeed = 999f;
    private float followSpeedMultiplier = 1f;

    public void SetFollowSpeedMultiplier(float mult) => followSpeedMultiplier = mult;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSlice();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StopSlice();
        }
        else if (slice)
        {
            ContinueSlice();
        }
    }

    private void StartSlice()
    {
        Vector3 bladePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        bladePosition.z = 0f;
        transform.position = bladePosition;

        slice = true;
        bladeCollider.enabled = true;
    }

    private void StopSlice()
    {
        slice = false;
        bladeCollider.enabled = false;
    }

    private void ContinueSlice()
    {
        Vector3 bladePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        bladePosition.z = 0f;

        direction = bladePosition - transform.position;
        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minVelocity;

        transform.position = bladePosition;
    }
}
