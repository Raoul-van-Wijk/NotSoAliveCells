using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton class thing for audio
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;

	private void Awake()
	{
        if (Instance != null && Instance != this)
            Destroy(this);
        else
		{
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void DestroyThis()
	{
        Destroy(this.gameObject);
	}

    public void PlaySound(AudioClip clip)
	{
        audioSource.PlayOneShot(clip);
	}

    public void PlayBackground(AudioClip clip)
    {
        audioSource.loop = true;
        audioSource.clip = clip;
        audioSource.Play();
    }
}
