using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
    public String Name = "Weapon Name";
    
    public BaseWeapon UpgradeA;
    public BaseWeapon UpgradeB;
    
    public abstract IWeaponEffect GetWeaponEffect();
}