using UnityEngine;

/// <summary>
/// Premium-currency pickup. Set the collider to Is Trigger and tag the player as Player.
/// </summary>
public class GemCollectible : MonoBehaviour
{
    [SerializeField, Min(1)] private int value = 1;
    [SerializeField] private float rotationSpeed = 90f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance != null)
            GameManager.Instance.AddGems(value);

        Destroy(gameObject);
    }
}
