using UnityEngine;

/// <summary>
/// Smooth third-person follow camera for the runner.
/// </summary>
public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -7f);
    [SerializeField, Min(0.1f)] private float smoothSpeed = 6f;
    [SerializeField] private bool lookAtTarget = true;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        if (lookAtTarget)
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(target.position + Vector3.up * 1.2f - transform.position),
                smoothSpeed * Time.deltaTime
            );
    }

    public void SetTarget(Transform newTarget) => target = newTarget;
}