using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    private UpgradeChooser[] _chosenUpgrades = new UpgradeChooser[6];
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
                var chosenWeaponToUpgrade = i;
                

                var chosenLevel = WeaponsManager.Instance.GetWeapon(chosenWeaponToUpgrade).currentLevel;

                if (chosenLevel + 1 < WeaponsManager.Instance.GetWeapon(chosenWeaponToUpgrade).levels.Length)
                {
                    _chosenUpgrades[i].upgradeID = chosenWeaponToUpgrade;
                    _chosenUpgrades[i].upgradeLevel = chosenLevel + 1;
                    weaponFound = true;
                }

                safety++;
            }

            if (safety >= 100)
            {
                _chosenUpgrades[i].upgradeID = WeaponsManager.Instance.GetWeapon(i).currentLevel;
                _chosenUpgrades[i].upgradeLevel = -1;
            }

            if (_chosenUpgrades[i].upgradeLevel == -1)
            {
                var weaponBase = WeaponsManager.Instance.GetWeapon(i);

                UI_Manager.Instance.SetUpgrade(i, weaponBase.image, weaponBase.name, 10, "Weapon Perfected!");
                UI_Manager.Instance.changeUIState(UI_Manager.UI_State.Upgrading);

            }
            else
            {
                var weapon = WeaponsManager.Instance.GetWeapon(_chosenUpgrades[i].upgradeID).levels[_chosenUpgrades[i].upgradeLevel];
                var weaponBase = WeaponsManager.Instance.GetWeapon(_chosenUpgrades[i].upgradeID);
                UI_Manager.Instance.SetUpgrade(i, weaponBase.image, weaponBase.name, _chosenUpgrades[i].upgradeLevel + 1, weapon.levelUpDescription);
                UI_Manager.Instance.changeUIState(UI_Manager.UI_State.Upgrading);
            }
        }
    }

    public void DoUpgrade(int ID)
    {
        if(_chosenUpgrades[ID].upgradeLevel == -1)
            return;

        UpgradeLabelBehaviour.selectedUpgrader = null;
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
