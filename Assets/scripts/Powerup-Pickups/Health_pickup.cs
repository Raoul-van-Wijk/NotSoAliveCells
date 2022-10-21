using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_pickup : MonoBehaviour
{

    private PlayerController healthValue;
    public GameObject pickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        healthValue.health += 10f;
        healthValue.maxHealth += 10f;
        healthValue.currentHealth += 10f;
        Destroy(pickup);
    }
}
