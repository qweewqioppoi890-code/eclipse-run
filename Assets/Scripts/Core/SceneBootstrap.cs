using UnityEngine;

/// <summary>
/// Optional helper for the gameplay scene. Ensures time scale and run state are reset.
/// Put this on a separate active GameObject in the gameplay scene.
/// </summary>
public class SceneBootstrap : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance != null)
            GameManager.Instance.BeginRun();
    }
}
