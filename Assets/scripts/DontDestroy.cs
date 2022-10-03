using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Update()
    {
        // When player is in gameover scene
        // Destroy player prefad to avoid loop
        if (GameObject.FindGameObjectWithTag("GameOver"))
        {
            Destroy(this.gameObject);
        } else
        {
            // when player is above
            if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                TeleportToSpawn();
            }
        }
    }

    private void TeleportToSpawn()
    {
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Spawnpoint").transform.position;
    }
}
