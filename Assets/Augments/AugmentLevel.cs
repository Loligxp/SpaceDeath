using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "AugmentLevel", menuName = "Augments/Level", order = 2)]

public class AugmentLevel : ScriptableObject
{
    public float fireRate;
    [TextArea]
    public string levelUpDescription;
    public DamageEffects damageEffect;
    public GameObject bulletPrefab;

    [EnumToggleButtons]
    public enum AugmentType
    {
        MainCannon,
        Broadside,
        Mine
    }

    public AugmentType type;

    public float bulletDamage;
    public float bulletSpeed;
    public int bulletPierce;

}
