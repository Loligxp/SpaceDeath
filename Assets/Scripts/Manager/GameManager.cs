using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector]public GameObject playerObject;

    public int playerLevel;
    [SerializeField]private float currentXP;
    [SerializeField]private float XPRequired;
    public bool screenShake;
    
    
    
    private void LevelUp()
    {
        currentXP = 0;
        XPRequired += playerLevel;
        playerLevel++;
        
        UpgradeManager.Instance.StartUpgrade();
    }

    private void Update()
    {
        if (currentXP >= XPRequired)
        {
            LevelUp();
        }
        
        screenShake = PlayerPrefs.GetInt("ScreenshakeToggle", 1) == 1? true:false;
    }

    public void GainXP(float xpAmount)
    {
        currentXP += xpAmount;
    }

    public float GetXPPercentage()
    {
        return currentXP / XPRequired;
    }
}
