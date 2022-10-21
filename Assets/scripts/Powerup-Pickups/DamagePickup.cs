using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : MonoBehaviour
{

    private PlayerCombat value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player")
		{
            value = collision.GetComponent<PlayerCombat>();
            value.damageMultiplier = 1.5f;
            Destroy(gameObject);
		}
    }
}
