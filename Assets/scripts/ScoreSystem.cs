using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static int score = 0;
    [SerializeField] private Text scoreText;
    // Start is called before the first frame update

    private void Awake()
    {
        scoreText.text = score.ToString();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        score += 1;
    //        Debug.Log(score);
    //        Destroy(other.gameObject);
    //        scoreText.text = score.ToString();

    //    }
    //}
}
