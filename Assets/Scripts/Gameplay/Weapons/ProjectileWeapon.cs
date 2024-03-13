using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Weapons/Projectile")]
public class ProjectileWeapon : BaseWeapon
{
    public enum MultiShotType
    {
        Single,
        Parallel,
        Arc,
        Guided,
    }

    public BulletComponent BulletPrefab;
        
    public MultiShotType MultiShot = MultiShotType.Single;
    public float Speed = 30;
    public float RateOfFire = 1;
    public int MultiShotCount = 1;

    public override IWeaponEffect GetWeaponEffect()
    {
        if (MultiShot == MultiShotType.Single)
            return new BasicProjectileWeaponEffect(BulletPrefab, Speed, RateOfFire);
        if (MultiShot == MultiShotType.Parallel)
            return new ParallelProjectileWeaponEffect(BulletPrefab, MultiShotCount, Speed, RateOfFire);
        if (MultiShot == MultiShotType.Guided)
            return new GuidedProjectileWeaponEffect(BulletPrefab, Speed, RateOfFire);
        throw new NotImplementedException();
    }
}