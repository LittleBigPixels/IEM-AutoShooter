using System;
using System.Collections.Generic;
using UnityEngine;

public class SweepProjectileWeaponEffect : IWeaponEffect
{
    private readonly BulletComponent m_prefab;
    private readonly int m_multiShotCount;
    private readonly float m_speed;
    private readonly float m_rateOfFire;

    private int lastAngle = 0;

    private float m_nextShotDelay = 0;

    public SweepProjectileWeaponEffect(BulletComponent prefab, int multiShotCount, float speed, float rateOfFire)
    {
        m_prefab = prefab;
        m_multiShotCount = multiShotCount;
        m_speed = speed;
        m_rateOfFire = rateOfFire;
    }

    public void UpdateAndShoot(Vector3 origin, Vector3 direction)
    {
        float delayBetweenShots = 1.0f / m_rateOfFire;
        m_nextShotDelay += Time.deltaTime;
        if (m_nextShotDelay > delayBetweenShots)
        {
            Shoot(origin, direction);
            m_nextShotDelay -= delayBetweenShots;
        }
    }

    private void Shoot(Vector3 origin, Vector3 direction)
    {
        BulletComponent bullet = GameObject.Instantiate<BulletComponent>(m_prefab);
        bullet.transform.position = origin;
        bullet.Velocity = Quaternion.AngleAxis(lastAngle, Vector3.up) * direction * m_speed;
        lastAngle = (lastAngle + 360 / m_multiShotCount) % 360;
    }
}