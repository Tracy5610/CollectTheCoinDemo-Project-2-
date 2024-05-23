using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (HighscoreManager.instance == null)
        {
            new GameObject("HighScoreManager").AddComponent<HighscoreManager>();
        }
    }

    // Load Game Scene 
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
