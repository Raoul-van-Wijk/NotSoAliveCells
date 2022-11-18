using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Text timerText;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeSprite();
        }

        UpdateTimerUI();
    }
    void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }

    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = hourCount + "h:" + minuteCount + "m:" + (int)secondsCount + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }
    }
}
