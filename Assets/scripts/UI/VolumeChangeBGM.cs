using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeChangeBGM : MonoBehaviour
{
    [SerializeField] private TMP_InputField sliderText;
    [SerializeField] private Slider slider;

    private MuteBGM muteBGM;
    [SerializeField] private Button muteButton;

    // Start is called before the first frame update
    void Start()
    {
        muteBGM = muteButton.GetComponent<MuteBGM>();

        // whenever a value changes fire code here, this also happens when it changes through the slider
        sliderText.onValueChanged.AddListener((v) =>
        {
            float vol = float.Parse(v);
            vol = Mathf.Round(Mathf.Clamp(vol, 0, 100));
            slider.value = vol;
            muteBGM.Unmute();
            AudioManager.Instance.SetMuteBGM();
        });
    }
}
