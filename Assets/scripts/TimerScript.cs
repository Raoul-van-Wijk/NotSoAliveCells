using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;


    public float runTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        runTime = t;
        int minutes = (int)(t / 60);
        int seconds = (int)Mathf.Floor(t % 60);

        timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
    