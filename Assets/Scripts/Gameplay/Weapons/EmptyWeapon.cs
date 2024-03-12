using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Empty")]
public class EmptyWeapon : BaseWeapon
{
    public override IWeaponEffect GetWeaponEffect()
    {
        return new DummyWeaponEffect();
    }
}