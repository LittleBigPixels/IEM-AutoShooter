using System;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponEffect
{
    void UpdateAndShoot(Vector3 origin, Vector3 direction);
}