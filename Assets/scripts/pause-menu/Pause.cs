using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public bool isGamePaused = false;

    private string previousButton;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject confirmation;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject rebindMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (rebindMenu.activeSelf)
			{
                rebindMenu.SetActive(false);
			}
            else if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        confirmation.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Confirm()
    {
        if (previousButton == "Restart")
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
          Time.timeScale = 1f;    
        }
        else if (previousButton == "MainMenu")
           SceneManager.LoadScene("MainMenu");
    }


    public void LoadMainMenu()
    {
        pauseMenu.SetActive(false);
        confirmation.SetActive(true);
        previousButton = "MainMenu";
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        pauseMenu.SetActive(false);
        confirmation.SetActive(true);
        previousButton = "Restart";
    }
}
