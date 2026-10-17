using UnityEngine;

/// <summary>
/// Marks a generated track segment and removes it after it falls behind the player.
/// </summary>
public class TrackSegment : MonoBehaviour
{
    [SerializeField] private float despawnBehindZ = 35f;

    private Transform player;

    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
    }

    private void Update()
    {
        if (player == null) return;

        if (transform.position.z < player.position.z - despawnBehindZ)
            Destroy(gameObject);
    }
}