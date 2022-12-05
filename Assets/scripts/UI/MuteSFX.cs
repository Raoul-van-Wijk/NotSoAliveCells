using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSFX : MonoBehaviour
{
    [SerializeField] private Button button;
	[SerializeField] private Image buttonImage;
	[SerializeField] private Sprite muted;
    [SerializeField] private Sprite unmuted;


    // Start is called before the first frame update
    void Awake()
    {
        button.onClick.AddListener(ToggleMute);
    }
    
    public void ToggleMute()
	{
        // toggles mute for SFX audio and changes sprite
        AudioManager.Instance.muteSFX = !AudioManager.Instance.muteSFX;
        if (AudioManager.Instance.muteSFX)
		{
            buttonImage.sprite = muted;
            PlayerPrefs.SetInt("MutedSFX", 1);
        }
        else
		{
            buttonImage.sprite = unmuted;
            PlayerPrefs.SetInt("MutedSFX", 0);
        }
    }

    public void Unmute()
	{
        buttonImage.sprite = unmuted;
        AudioManager.Instance.muteSFX = false;
        PlayerPrefs.SetInt("MutedSFX", 0);
    }
}
