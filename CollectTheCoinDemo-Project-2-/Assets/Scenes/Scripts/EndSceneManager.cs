using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    public Text finalScoreText;
    public Text highScoresText;

    private void Start()
    {
        // Get the final score from the CoinCounter instance
        int finalScore = CoinCounter.instance.GetCurrentScore();
        finalScoreText.text = "Final Score: " + finalScore;

        // Retrieve and display the highest score
        int highestScore = HighscoreManager.instance.GetHighscore();
        highScoresText.text = "High Score: " + highestScore;
    }

    // Method to restart the game
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Method to return to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
