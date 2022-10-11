using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    // public WeaponScriptableObject activeWeapon;

    public WeaponScriptableObject weaponSlot1,
                                 weaponSlot2;

    public Transform attackPoint;
    public LayerMask enemyLayers;


    /// <summary>
    /// Variable used to keep track of next attack time
    /// </summary>
    private float nextAttackTime1,
                  nextAttackTime2 = 0f;

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
                enemy.GetComponent<EnemyTestScript>().ManageDamage(activeWeapon.attackDamage * activeWeapon.critRateModifier, activeWeapon.statusEffect);
            }
            else
            {
                enemy.GetComponent<EnemyTestScript>().ManageDamage(activeWeapon.attackDamage, activeWeapon.statusEffect);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, weaponSlot1.attackRange);
    }
}
