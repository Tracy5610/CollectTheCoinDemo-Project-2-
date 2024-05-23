using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopHighScoreDisplay : MonoBehaviour
{
    public Text topHighScoreText;

    private void Start()
    {
        if (topHighScoreText == null)
        {
            Debug.LogError("TopHighScoreText is not assigned in the inspector.");
            return;
        }
        UpdateTopHighScore();
    }

    // Using the HighscoreManager script to pull out the highscore 
    private void UpdateTopHighScore()
    {
        int topScore = HighscoreManager.instance.GetHighscore();
        topHighScoreText.text = "Top High Score: " + topScore;
    }
}
