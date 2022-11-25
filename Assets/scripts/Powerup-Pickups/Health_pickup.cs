using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_pickup : MonoBehaviour
{

    private PlayerController healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player")
		{
            healthValue = collision.GetComponent<PlayerController>();
            healthValue.health += 10f;
            healthValue.maxHealth += 10f;
            healthValue.currentHealth += 10f;
            healthValue.SetSlider();
            Destroy(gameObject);
		}
    }
}
