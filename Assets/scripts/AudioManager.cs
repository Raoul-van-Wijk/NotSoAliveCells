using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip finishSound;
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip attackSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	void Awake()
	{
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void DeathSound()
	{
        audioSource.PlayOneShot(deathSound, 0.3f);
	}

    public void DashSound()
    {
        audioSource.PlayOneShot(dashSound, 1f);
    }

    public void JumpSound()
    {
        audioSource.PlayOneShot(jumpSound, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
