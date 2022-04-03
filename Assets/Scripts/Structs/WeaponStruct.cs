using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct WeaponStruct
{
    public string weaponName;
    public Sprite weaponImage;
    public WeaponLevel[] weaponLevels;
    public int currentLevel;
    public float fireCooldown;
    public bool fixedWeapon;
}

[System.Serializable]
public struct WeaponLevel
{
    public float fireRate;
    public string levelUpDescription;
    public DamageEffects damageEffect;
    public GameObject bulletPrefab;
}
