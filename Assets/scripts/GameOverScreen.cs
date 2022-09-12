using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public const int startScene = 1;
    public Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "Score:" + score.ToString();
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(startScene);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
