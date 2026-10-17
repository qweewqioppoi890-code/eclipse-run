using UnityEngine;

/// <summary>
/// Collectible coin. Set the object's collider to Is Trigger and tag the player as Player.
/// </summary>
public class CoinCollectible : MonoBehaviour
{
    [SerializeField, Min(1)] private int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance != null)
            GameManager.Instance.AddCoins(value);

        Destroy(gameObject);
    }
}