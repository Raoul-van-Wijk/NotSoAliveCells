using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    // public WeaponScriptableObject activeWeapon;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private SpriteRenderer weapon1SR;
    [SerializeField] private SpriteRenderer weapon2SR;
    [SerializeField] private Animator leftHandAnimator;
    [SerializeField] private Animator rightHandAnimator;

    [SerializeField] private Image UiWeaponLeft;
    [SerializeField] private Image UiWeaponRight;

    [SerializeField] private AudioClip[] attackAudios;
    [SerializeField] private AudioClip equipAudio;

    private bool flipped = false;

    public WeaponScriptableObject weaponSlot1,
                                 weaponSlot2;
                                 
    private SwapWeapon weaponPickUp;
    private GameObject newWeapon;

    public LayerMask playerLayer;

    public TextMeshProUGUI weaponName1,
                           weaponName2,
                           pickupName;

    public TextMeshProUGUI weaponEffect1,
                           weaponEffect2,
                           pickupEffect;
    public GameObject weaponSwapUI;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float damageMultiplier;

    private Collider2D inRange;

    [SerializeField] LayerMask weaponLayer;
    public bool UIOpened = false;

    /// <summary>
    /// Variable used to keep track of next attack time
    /// </summary>
    private float nextAttackTime1,
                  nextAttackTime2 = 0f;

	void Start()
	{
        weapon1SR.sprite = weaponSlot1.weapenSprite;
        weapon2SR.sprite = weaponSlot2.weapenSprite;
        UiWeaponLeft.sprite = weaponSlot1.weapenSprite;
        UiWeaponRight.sprite = weaponSlot2.weapenSprite;

    }

    public void Equip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inRange = Physics2D.OverlapCircle(transform.position, 2.5f, weaponLayer);

            if (!UIOpened && inRange)
            {
                newWeapon = inRange.gameObject;
                weaponPickUp = inRange.gameObject.GetComponent<SwapWeapon>();
                OpenSwapWeaponUI(weaponPickUp.weaponPickUp);
            } else
            {
                CloseSwapWeaponUI();
            }
        }
    }

    public void SwapWeapon1(InputAction.CallbackContext context)
	{
        if (UIOpened)
		{
            // weapon to be picked up
            WeaponScriptableObject swap = weaponPickUp.weaponPickUp;
            CloseSwapWeaponUI();

            AudioManager.Instance.PlaySound(equipAudio);

            // old weapon put back in the pickup weapon object
            weaponPickUp.weaponPickUp = weaponSlot1;
            // changes the sprite of the swap weapon object on ground
            weaponPickUp.sr.sprite = weaponSlot1.weapenSprite;
            // new weapon is put in the weaponSlot
            weaponSlot1 = swap;
            // changes sprite of weapon in hand, if player is flipped, change the other hand's sprite
            if (flipped)
                weapon2SR.sprite = swap.weapenSprite;
            else
                weapon1SR.sprite = swap.weapenSprite;
            // changes sprite of ui weapon
            UiWeaponLeft.sprite = swap.weapenSprite;

            // destroy swap weapon object if empty
            if (weaponPickUp.weaponPickUp.weaponName == "Empty")
			{
                Destroy(newWeapon);
			}
		}
    }

    // other hand
    public void SwapWeapon2(InputAction.CallbackContext context)
    {
        if (UIOpened)
        {
            WeaponScriptableObject swap = weaponPickUp.weaponPickUp;
            CloseSwapWeaponUI();

            AudioManager.Instance.PlaySound(equipAudio);

            weaponPickUp.weaponPickUp = weaponSlot2;
            weaponPickUp.sr.sprite = weaponSlot2.weapenSprite;
            weaponSlot2 = swap;

            if (flipped)
                weapon1SR.sprite = swap.weapenSprite;
            else
                weapon2SR.sprite = swap.weapenSprite;
            UiWeaponRight.sprite = swap.weapenSprite;

            if (weaponPickUp.weaponPickUp.weaponName == "Empty")
            {
                Destroy(newWeapon);
            }
        }
    }

    public void Attack(InputAction.CallbackContext context)
	{
        if (context.performed && Time.time >= nextAttackTime1)
        {
            AttackWeapon(weaponSlot1);

			AudioManager.Instance.PlaySound(attackAudios[Random.Range(0, 3)]);

			// flipped player
			if (!flipped)
                leftHandAnimator.SetTrigger("Attack");
            else
                rightHandAnimator.SetTrigger("Attack");

            nextAttackTime1 = Time.time + 1f / weaponSlot1.attackRate;
        }
    }

    public void Attack2(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextAttackTime2)
		{
            AttackWeapon(weaponSlot2);

            AudioManager.Instance.PlaySound(attackAudios[Random.Range(0, 3)]);

            // flipped player
            if (!flipped)
                rightHandAnimator.SetTrigger("Attack");
            else
                leftHandAnimator.SetTrigger("Attack");

            nextAttackTime2 = Time.time + 1f / weaponSlot2.attackRate;
        }
    }

    /// <summary>
    /// Function used to deal dmg to all the enemies that where in range of hitarea when attack was triggered
    /// </summary>
    /// <param name="activeWeapon">Weapon that caused attack trigger</param>
    private void AttackWeapon(WeaponScriptableObject activeWeapon)
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, activeWeapon.attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (Random.Range(0f, 100f) <= activeWeapon.critRate)
            {
                enemy.GetComponent<EnemyTestScript>().ManageDamage((activeWeapon.attackDamage * activeWeapon.critRateModifier) * damageMultiplier, activeWeapon.statusEffect);
            }
            else
            {
                enemy.GetComponent<EnemyTestScript>().ManageDamage(activeWeapon.attackDamage * damageMultiplier, activeWeapon.statusEffect);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, weaponSlot1.attackRange);
    }

    private void OpenSwapWeaponUI(WeaponScriptableObject weapon)
    {
        UIOpened = true;
        weaponName1.SetText(weaponSlot1.weaponName);
        weaponName2.SetText(weaponSlot2.weaponName);
        pickupName.SetText(weapon.weaponName);

        weaponEffect1.SetText(weaponSlot1.statusEffect.ToString());
        weaponEffect2.SetText(weaponSlot2.statusEffect.ToString());
        pickupEffect.SetText(weapon.statusEffect.ToString());

        weaponSwapUI.SetActive(true);
    }

    private void CloseSwapWeaponUI()
    {
        UIOpened = false;
        weaponSwapUI.SetActive(false);
    }

    public void FlipWeapons()
	{
        leftHandAnimator.Play("Idle");
        rightHandAnimator.Play("Idle");
        Sprite swap = weapon1SR.sprite;
		weapon1SR.sprite = weapon2SR.sprite;
		weapon2SR.sprite = swap;
        flipped = !flipped;
	}
}
