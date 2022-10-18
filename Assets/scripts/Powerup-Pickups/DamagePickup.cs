using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : MonoBehaviour
{

    private PlayerCombat value;
    public GameObject pickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
            value = collision.GetComponent<PlayerCombat>();
            value.damageMultiplier = 1.5f;
            Debug.Log("work");
            Destroy(pickup);
    }
}
