using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSliderBGM : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TMP_InputField sliderText;


    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        // whenever a value changes fire code here, this also happens when it changes through the text input
        slider.onValueChanged.AddListener((v) =>
        {
            v = Mathf.Round(Mathf.Clamp(v, 0, 100));
            sliderText.text = v.ToString("0");
            AudioManager.Instance.volumeBGM = v / 100;
            AudioManager.Instance.ChangeBGMVolume();
            PlayerPrefs.SetInt("VolumeBGM", (int)v);
        });
    }
}
