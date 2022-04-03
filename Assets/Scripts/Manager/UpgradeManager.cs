using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    private UpgradeChooser[] _chosenUpgrades = new UpgradeChooser[3];
    public bool isUpgrading = false;

    
    public void StartUpgrade()
    {
        isUpgrading = true;

        for (int i = 0; i < _chosenUpgrades.Length; i++)
        {
            var weaponFound = false;
            int safety = 0;
            while (weaponFound == false && safety < 100)
            {
                var chosenWeaponToUpgrade = WeaponsManager.Instance.GetRandomWeaponID();
                var chosenLevel = WeaponsManager.Instance.GetWeapon(chosenWeaponToUpgrade).currentLevel;

                if (chosenLevel + 1 < WeaponsManager.Instance.GetWeapon(chosenWeaponToUpgrade).weaponLevels.Length)
                {
                    _chosenUpgrades[i].upgradeID = chosenWeaponToUpgrade;
                    _chosenUpgrades[i].upgradeLevel = chosenLevel + 1;
                    weaponFound = true;
                }

                safety++;
            }

            if (safety >= 100)
            {
                _chosenUpgrades[i].upgradeID = 0;
                _chosenUpgrades[i].upgradeLevel = 0;
            }

            var weapon = WeaponsManager.Instance.GetWeapon(_chosenUpgrades[i].upgradeID).weaponLevels[_chosenUpgrades[i].upgradeLevel];
            var weaponBase = WeaponsManager.Instance.GetWeapon(_chosenUpgrades[i].upgradeID);
            UI_Manager.Instance.SetUpgrade(i,weaponBase.weaponImage,weapon.levelUpDescription,  weaponBase.weaponName +  " Level " + (_chosenUpgrades[i].upgradeLevel + 1));
            UI_Manager.Instance.changeUIState(UI_Manager.UI_State.Upgrading);
        }
    }

    public void DoUpgrade(int ID)
    {
        WeaponsManager.Instance.UpgradeWeapon(_chosenUpgrades[ID].upgradeID,_chosenUpgrades[ID].upgradeLevel);
        isUpgrading = false;
        UI_Manager.Instance.changeUIState(UI_Manager.UI_State.None);
    }
}

[System.Serializable]
public struct UpgradeChooser
{
    public int upgradeID;
    public int upgradeLevel;
}
