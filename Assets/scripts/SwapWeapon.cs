using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SwapWeapon : MonoBehaviour
{
    public LayerMask playerLayer;
    private Transform weaponTransform;
    public PlayerCombat playerCombat;

    private WeaponScriptableObject slot1,
                                 slot2;
    public WeaponScriptableObject WeaponPickUp;

    public TextMeshProUGUI weaponName1,
                           weaponName2,
                           pickupName;

    public TextMeshProUGUI weaponEffect1,
                           weaponEffect2,
                           pickupEffect;
    public GameObject weaponSwapUI;

    private SpriteRenderer sr;

    private bool UIOpened = false;
    private void Awake()
    {
        slot1 = playerCombat.weaponSlot1;
        slot2 = playerCombat.weaponSlot2;
        // sr = gameObject.GetComponent<SpriteRenderer>();
        // sr.sprite = WeaponPickUp.weapenSprite;
        weaponTransform = GetComponent<Transform>();
    }

    private float UIDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool inRange = Physics2D.OverlapCircle(transform.position, 2.5f, playerLayer);
        if(UIOpened)
        {
            SwapWeaponSlot();
        }
        if (inRange)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !UIOpened)
            {
                OpenSwapWeaponUI();
            }
            else if (Input.GetKeyDown(KeyCode.E) && UIOpened)
            {
                CloseSwapWeaponUI();
            }
        } 
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            CloseSwapWeaponUI();
        }


    }

    private void SwapWeaponSlot()
    {
        WeaponScriptableObject swap = WeaponPickUp;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CloseSwapWeaponUI();
            WeaponPickUp = slot1;
            // sr.sprite = slot1.weapenSprite;
            playerCombat.weaponSlot1 = swap;
            slot1 = swap;
        } 
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            CloseSwapWeaponUI();
            WeaponPickUp = slot2;
            // sr.sprite = slot2.weapenSprite;
            playerCombat.weaponSlot2 = swap;
            slot2 = swap;
        }

        if(WeaponPickUp.weaponName == "Empty")
        {
            Destroy(gameObject);
        }
    }

    private void OpenSwapWeaponUI()
    { 
        UIOpened = true;
        weaponName1.SetText(slot1.weaponName);
        weaponName2.SetText(slot2.weaponName);
        pickupName.SetText(WeaponPickUp.weaponName);

        weaponEffect1.SetText(slot1.statusEffect.ToString());
        weaponEffect2.SetText(slot2.statusEffect.ToString());
        pickupEffect.SetText(WeaponPickUp.statusEffect.ToString());

        weaponSwapUI.SetActive(true);
    }

    private void CloseSwapWeaponUI()
    {
        UIOpened = false;
        weaponSwapUI.SetActive(false);
    }
}
