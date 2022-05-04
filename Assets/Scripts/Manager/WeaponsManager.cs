using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class WeaponsManager : MonoSingleton<WeaponsManager>
{
    [SerializeField] private AugmentContainer[] augment;
    [SerializeField] private GameObject basicBullet;
    
    public int GetRandomAugmentID()
    {
        return Random.Range(0, augment.Length);
    }

    public AugmentContainer GetWeapon(int ID)
    {
        return augment[ID];
    }

    public void UpgradeWeapon(int weaponID, int newLevel)
    {
        augment[weaponID].currentLevel = newLevel;
    }

    private void Start()
    {
        for (int i = 0; i < augment.Length; i++)
        {
            augment[i].currentLevel = -1;
        }
    }

    private void Update()
    {
        for (int i = 0; i < augment.Length; i++)
        {
            augment[i].fireCooldown += Time.deltaTime;
        }
    }

    public void ShootPrimaryWeapons(Transform weaponCaller, Transform VelocityTransform)
    {
        for (int i = 0; i < augment.Length; i++)
        {
            if (augment[i].currentLevel < 0) continue;
            if (!(augment[i].fireCooldown > 1 / augment[i].levels[augment[i].currentLevel].fireRate)) continue;
            
            augment[i].fireCooldown = 0;

            switch (augment[i].levels[augment[i].currentLevel].type)
            {
                case AugmentLevel.AugmentType.MainCannon:
                case AugmentLevel.AugmentType.Mine:
                    ShootMainCannon(weaponCaller,VelocityTransform, augment[i]);
                    break;
                case AugmentLevel.AugmentType.Broadside:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            //if(!augment[i].fixedWeapon)
            //    Instantiate(augment[i].levels[augment[i].currentLevel].bulletPrefab, weaponCaller.position, weaponCaller.rotation);
            //if(augment[i].fixedWeapon)
            //    Instantiate(augment[i].levels[augment[i].currentLevel].bulletPrefab, weaponCaller.position, VelocityTransform.rotation);
        }
    }

    public void ShootMainCannon(Transform weaponCaller, Transform VelocityTransform, AugmentContainer augment)
    {
        var bullet = Instantiate(augment.levels[augment.currentLevel].bulletPrefab, weaponCaller.position, weaponCaller.rotation);
        var bulletScript = bullet.GetComponent<Bullet>();
        
        bulletScript.SetValues(augment.levels[augment.currentLevel].bulletDamage,augment.levels[augment.currentLevel].bulletSpeed, augment.levels[augment.currentLevel].bulletPierce);
    }
}
