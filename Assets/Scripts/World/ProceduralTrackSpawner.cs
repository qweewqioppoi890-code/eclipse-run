using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Repeatedly spawns track prefabs ahead of the runner.
/// Create a segment prefab with a collider/visual and TrackSegment component.
/// </summary>
public class ProceduralTrackSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TrackSegment segmentPrefab;
    [SerializeField, Min(10f)] private float segmentLength = 30f;
    [SerializeField, Min(2)] private int segmentsAhead = 6;
    [SerializeField] private float trackCenterZ = 0f;

    private readonly Queue<TrackSegment> spawned = new Queue<TrackSegment>();
    private float nextSpawnZ;

    private void Start()
    {
        if (player == null || segmentPrefab == null)
        {
            Debug.LogError("ProceduralTrackSpawner needs a Player and Segment Prefab.", this);
            enabled = false;
            return;
        }

        nextSpawnZ = trackCenterZ;
        for (int i = 0; i < segmentsAhead; i++)
            SpawnSegment();
    }

    private void Update()
    {
        if (player == null || segmentPrefab == null) return;

        float targetZ = player.position.z + segmentLength * (segmentsAhead - 1);
        while (nextSpawnZ < targetZ)
            SpawnSegment();
    }

    private void SpawnSegment()
    {
        TrackSegment segment = Instantiate(
            segmentPrefab,
            new Vector3(0f, 0f, nextSpawnZ),
            Quaternion.identity
        );
        segment.Initialize(player);
        spawned.Enqueue(segment);
        nextSpawnZ += segmentLength;
    }
}