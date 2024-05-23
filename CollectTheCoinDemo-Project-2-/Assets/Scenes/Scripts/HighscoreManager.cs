using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;

    private int highestScore;

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
        LoadHighscore();
    }

    public void AddNewScore(int score)
    {
        int savedHighscore = PlayerPrefs.GetInt("Highscore", 0);

        // Compare the current score with the saved high score
        if (score > savedHighscore)
        {
            highestScore = score; // Update the highest score
            SaveHighscore(); // Save the updated high score to PlayerPrefs
        }
    }

    public int GetHighscore()
    {
        return highestScore;
    }

    private void SaveHighscore()
    {
        PlayerPrefs.SetInt("Highscore", highestScore);
        PlayerPrefs.Save(); // Ensure the changes are saved immediately
    }

    private void LoadHighscore()
    {
        highestScore = PlayerPrefs.GetInt("Highscore", 0);
    }
}
