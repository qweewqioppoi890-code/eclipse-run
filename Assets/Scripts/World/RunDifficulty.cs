using UnityEngine;

/// <summary>
/// Gradually increases runner speed during a run. Attach beside RunnerController.
/// </summary>
[RequireComponent(typeof(RunnerController))]
public class RunDifficulty : MonoBehaviour
{
    [SerializeField, Min(1f)] private float startingSpeed = 8f;
    [SerializeField, Min(0f)] private float speedIncreasePerMinute = 1.2f;
    [SerializeField, Min(1f)] private float maximumSpeed = 16f;

    private RunnerController runner;
    private float elapsed;

    private void Awake()
    {
        runner = GetComponent<RunnerController>();
        runner.SetForwardSpeed(startingSpeed);
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        elapsed += Time.deltaTime;
        float speed = Mathf.Min(maximumSpeed, startingSpeed + elapsed / 60f * speedIncreasePerMinute);
        runner.SetForwardSpeed(speed);
    }
}
