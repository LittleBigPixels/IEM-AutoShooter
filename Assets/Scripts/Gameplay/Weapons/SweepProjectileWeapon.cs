using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Sweep Projectile")]
public class SweepProjectileWeapon : BaseWeapon
{    
    public BulletComponent BulletPrefab;

    public Gradient Gradient;
    
    public float BulletSpeed = 30;
    public float RateOfFire = 1;
 
    public float RotationSpeed = 5;
    
    public override IWeaponEffect GetWeaponEffect()
    {
        return new SweepWeaponEffect(BulletPrefab, Gradient, BulletSpeed, RateOfFire, RotationSpeed);
    }
}