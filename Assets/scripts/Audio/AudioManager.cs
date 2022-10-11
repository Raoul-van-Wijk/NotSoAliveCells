using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton class thing for audio
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSourceSFX;
    [SerializeField] private AudioSource audioSourceBGM;
    public float volumeSFX = 1;
    public float volumeBGM = 1;

    public bool muteSFX = false;
    public bool muteBGM = false;

    private void Awake()
	{
        if (Instance != null && Instance != this)
            Destroy(this);
        else
		{
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void DestroyThis()
	{
        Destroy(gameObject);
	}

    public void PlaySound(AudioClip clip)
    {
        if (!muteSFX)
            audioSourceSFX.PlayOneShot(clip, volumeSFX);
	}

    public void PlayBackground(AudioClip clip)
    {
        audioSourceBGM.loop = true;
        audioSourceBGM.clip = clip;
        audioSourceBGM.volume = volumeBGM;
        audioSourceBGM.Play();
    }

    public void ChangeBGMVolume()
	{
        audioSourceBGM.volume = volumeBGM;
	}

    // sets the bgm mute to that of muteBGM
    public void SetMuteBGM()
	{
        audioSourceBGM.mute = muteBGM;
	}
}
