using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteBGM : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite muted;
    [SerializeField] private Sprite unmuted;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ToggleMute);
    }

    public void ToggleMute()
    {
        // Toggle mute BGM audio and changes sprite
        AudioManager.Instance.muteBGM = !AudioManager.Instance.muteBGM;
        AudioManager.Instance.SetMuteBGM();
        if (AudioManager.Instance.muteBGM)
        {
            buttonImage.sprite = muted;
            PlayerPrefs.SetInt("MutedBGM", 1);
        }
        else
		{
            buttonImage.sprite = unmuted;
            PlayerPrefs.SetInt("MutedBGM", 0);
        }
    }

    public void Unmute()
    {
        buttonImage.sprite = unmuted;
        AudioManager.Instance.muteBGM = false;
        AudioManager.Instance.SetMuteBGM();
        PlayerPrefs.SetInt("MutedBGM", 0);
    }
}
