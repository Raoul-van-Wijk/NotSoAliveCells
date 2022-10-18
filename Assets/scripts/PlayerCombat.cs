using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    // public WeaponScriptableObject activeWeapon;

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
            WeaponScriptableObject swap = weaponPickUp.weaponPickUp;
            CloseSwapWeaponUI();
            weaponPickUp.weaponPickUp = weaponSlot1;
            weaponSlot1 = swap;

            if (weaponPickUp.weaponPickUp.weaponName == "Empty")
			{
                Destroy(newWeapon);
			}
		}
    }

    public void SwapWeapon2(InputAction.CallbackContext context)
    {
        if (UIOpened)
        {
            WeaponScriptableObject swap = weaponPickUp.weaponPickUp;
            CloseSwapWeaponUI();
            weaponPickUp.weaponPickUp = weaponSlot2;
            weaponSlot2 = swap;

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
            nextAttackTime1 = Time.time + 1f / weaponSlot1.attackRate;
        }
    }

    public void Attack2(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextAttackTime2)
        {
            AttackWeapon(weaponSlot2);
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
}
