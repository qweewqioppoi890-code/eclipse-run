using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Minimal score/game-over UI. Assign labels and a game-over panel in the Inspector.
/// The panel should start inactive and contain a Button wired to RestartRun.
/// </summary>
public class RunStatusHUD : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Text distanceText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text finalDistanceText;

    private float startZ;
    private bool shownGameOver;

    private void Start()
    {
        if (player != null) startZ = player.position.z;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        float distance = player != null ? Mathf.Max(0f, player.position.z - startZ) : 0f;

        if (distanceText != null)
            distanceText.text = "Distance: " + Mathf.FloorToInt(distance) + " m";

        if (!shownGameOver && GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            shownGameOver = true;
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
            if (finalDistanceText != null)
                finalDistanceText.text = "Run distance: " + Mathf.FloorToInt(distance) + " m";
        }
    }

    public void RestartRun()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance != null)
            GameManager.Instance.RestartRun();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
