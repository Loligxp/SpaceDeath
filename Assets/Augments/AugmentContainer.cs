using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AugmentContainer", menuName = "Augments/Container", order = 1)]
public class AugmentContainer : ScriptableObject
{
    public new string name;
    public Sprite image;
    public AugmentLevel[] levels;
    public int currentLevel;
    public float fireCooldown;
    public bool fixedWeapon;
}