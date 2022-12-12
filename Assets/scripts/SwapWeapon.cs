using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class SwapWeapon : MonoBehaviour
{
    public WeaponScriptableObject weaponPickUp;
    private LayerMask playerLayer;
    public SpriteRenderer sr;

    private void Awake()
    {
        playerLayer = LayerMask.GetMask("Player");
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = weaponPickUp.weapenSprite;
        // sr.sprite = weaponPickUp.weapenSprite;
    }

	private void Update()
	{
        if (Physics2D.OverlapCircle(transform.position, 2.5f, playerLayer))
		{
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        } else
		{
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
}
