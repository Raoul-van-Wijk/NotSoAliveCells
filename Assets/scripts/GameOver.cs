using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public Text pointsText;
    private int score;

    public void Awake()
    {
        score = ScoreSystem.score;
    }
    public void Start()
    {
        gameObject.SetActive(true);
        pointsText.text = "Youre score is:" + score.ToString();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
