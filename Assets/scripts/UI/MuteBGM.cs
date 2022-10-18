using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteBGM : MonoBehaviour
{
    private Button button;
    private Image buttonImage;
    [SerializeField] private Sprite muted;
    [SerializeField] private Sprite unmuted;


    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        buttonImage = gameObject.GetComponent<Image>();
        button.onClick.AddListener(Mute);
    }

    public void Mute()
    {
        // unmute/mute all SFX audio and change sprite to unmuted/muted
        AudioManager.Instance.muteBGM = !AudioManager.Instance.muteBGM;
        AudioManager.Instance.SetMuteBGM();
        if (AudioManager.Instance.muteBGM)
            buttonImage.sprite = muted;
        else
            buttonImage.sprite = unmuted;
    }

    public void Unmute()
    {
        buttonImage.sprite = unmuted;
        AudioManager.Instance.muteBGM = false;
        AudioManager.Instance.SetMuteBGM();
    }
}
