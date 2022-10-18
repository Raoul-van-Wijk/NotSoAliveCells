using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptOptionsMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (var res in Screen.resolutions)
			{
                Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
			}
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Screen.fullScreenMode != FullScreenMode.FullScreenWindow)
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            else
                Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
