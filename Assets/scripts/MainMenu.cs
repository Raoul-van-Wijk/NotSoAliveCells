using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject rebindMenu;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject quitButton;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        //Debug.Log("sss");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (rebindMenu.activeSelf)
            {
                rebindMenu.SetActive(false);
            }
            else if (optionsMenu.activeSelf)
            {
                Return();
            }
        }
    }

    public void Return()
	{
        optionsMenu.SetActive(false);
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        quitButton.SetActive(true);
    }
}
