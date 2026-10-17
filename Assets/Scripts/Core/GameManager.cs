using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Minimal run-state manager for Eclipse Run.
/// Add this to a GameObject named GameManager in the first scene.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Coins { get; private set; }
    public int Gems { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCoins(int amount)
    {
        if (amount > 0) Coins += amount;
    }

    public void AddGems(int amount)
    {
        if (amount > 0) Gems += amount;
    }

    public void EndRun()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
    }

    public void RestartRun()
    {
        Time.timeScale = 1f;
        IsGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BeginRun()
    {
        Time.timeScale = 1f;
        IsGameOver = false;
    }
}