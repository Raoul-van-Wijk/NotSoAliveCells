using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffects
{
    none,
    fire,
    poisen,
    freeze,
    electric,
}

[CreateAssetMenuAttribute(fileName = "Weapons", menuName = "ScriptableObjects/Weapons")]
public class WeaponScriptableObject : ScriptableObject
{ 
    public enum WeaponType
    {
        sword,
        bow,
    };

    public string weaponName;
    public Sprite weapenSprite;

    public WeaponType weaponType;

    public StatusEffects statusEffect;

    public float attackRange;
    public float attackDamage;
    public float attackRate;

    public float critRate;
    public float critRateModifier;

    public float chance;

}
