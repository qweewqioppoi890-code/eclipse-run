using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the current coin and gem totals using Unity UI Text components.
/// Assign one or both labels in the Inspector.
/// </summary>
public class CoinHUD : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Text gemsText;
    [SerializeField] private string coinsPrefix = "Coins: ";
    [SerializeField] private string gemsPrefix = "Gems: ";

    private int lastCoins = int.MinValue;
    private int lastGems = int.MinValue;

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (coinsText != null && lastCoins != GameManager.Instance.Coins)
        {
            lastCoins = GameManager.Instance.Coins;
            coinsText.text = coinsPrefix + lastCoins;
        }

        if (gemsText != null && lastGems != GameManager.Instance.Gems)
        {
            lastGems = GameManager.Instance.Gems;
            gemsText.text = gemsPrefix + lastGems;
        }
    }
}
