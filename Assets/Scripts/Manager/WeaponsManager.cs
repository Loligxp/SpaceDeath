using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class WeaponsManager : MonoSingleton<WeaponsManager>
{
    [SerializeField] private WeaponStruct[] weapons;

    public int GetRandomWeaponID()
    {
        return Random.Range(0, weapons.Length);
    }

    public WeaponStruct GetWeapon(int ID)
    {
        return weapons[ID];
    }

    public void UpgradeWeapon(int weaponID, int newLevel)
    {
        weapons[weaponID].currentLevel = newLevel;
    }

    private void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].currentLevel = -1;
        }
    }

    private void Update()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].fireCooldown += Time.deltaTime;
        }
    }

    public void ShootPrimaryWeapons(Transform weaponCaller, Transform VelocityTransform)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].currentLevel < 0) continue;
            if (!(weapons[i].fireCooldown > 1 / weapons[i].weaponLevels[weapons[i].currentLevel].fireRate)) continue;
            
            weapons[i].fireCooldown = 0;
            
            if(!weapons[i].fixedWeapon)
                Instantiate(weapons[i].weaponLevels[weapons[i].currentLevel].bulletPrefab, weaponCaller.position, weaponCaller.rotation);
            if(weapons[i].fixedWeapon)
                Instantiate(weapons[i].weaponLevels[weapons[i].currentLevel].bulletPrefab, weaponCaller.position, VelocityTransform.rotation);
        }
    }
}
