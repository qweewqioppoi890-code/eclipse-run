using UnityEngine;

/// <summary>
/// Optional component for a track-segment prefab.
/// Assign obstacle prefabs that have a collider and the "Obstacle" tag.
/// Each segment instance places a small randomized set of obstacles.
/// </summary>
public class TrackObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField, Min(0)] private int minObstacles = 1;
    [SerializeField, Min(0)] private int maxObstacles = 3;
    [SerializeField] private float firstSpawnZ = 12f;
    [SerializeField] private float spawnSpacing = 12f;
    [SerializeField] private float laneOffset = 2.5f;
    [SerializeField] private float lateralJitter = 0f;
    [SerializeField] private float obstacleY = 0.5f;
    [SerializeField] private float segmentLength = 30f;

    private void Start()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0)
            return;

        int count = Random.Range(minObstacles, maxObstacles + 1);
        for (int i = 0; i < count; i++)
        {
            float z = firstSpawnZ + i * spawnSpacing;
            if (z >= segmentLength)
                break;

            int lane = Random.Range(-1, 2);
            float x = lane * laneOffset + Random.Range(-lateralJitter, lateralJitter);
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            if (prefab == null)
                continue;

            Vector3 localPosition = new Vector3(x, obstacleY, z);
            Instantiate(prefab, transform.TransformPoint(localPosition),
                transform.rotation, transform);
        }
    }
}
