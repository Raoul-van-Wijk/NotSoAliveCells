using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private Transform playerTransform;
    float t = 0f;
    private bool inRangeOfChest
    {
        get
        {
            return Vector2.Distance(transform.position, playerTransform.position) < 1.5f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRangeOfChest)
        {
            // show E on screen


        }
    }


    
}
