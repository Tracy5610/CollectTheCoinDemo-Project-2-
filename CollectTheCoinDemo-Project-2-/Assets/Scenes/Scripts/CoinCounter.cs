using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;

    public Text coinText;
    private int coinCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (coinText == null)
        {
            Debug.LogError("coinText is not assigned in the inspector.");
        }
        UpdateCoinText();
    }

    public void ResetCoins()
    {
        coinCount = 0;
        UpdateCoinText();
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateCoinText();
    }

    public int GetCurrentScore() // Ensure this method is present
    {
        return coinCount;
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
        else
        {
            Debug.LogError("coinText is null. Cannot update coin text.");
        }
    }

}
